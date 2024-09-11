using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
     float speed = 0.05f;
    

    private Transform targetPos;

    Vector3 direction;



    void Start()
    {
        Init();
    }

    void Update()
    {
        transform.position += direction * speed; // �ش� �������� �ӵ��� ���� �̵�
        DestroyMe();

    }



    /// <summary>
    /// ���� �� �ʱⰪ ������ ���� �Լ�.
    /// </summary>
    void Init()
    {

  
        //ȭ�� �� 4���� ���� �� �ϳ��� ������ �ϰ� ����
        int type = Random.Range(0, 4);
        //x��ǥ, y��ǥ ���� ���� ������ ��Ƶα�
        float randX = Random.Range(-4.5f, 4.5f);
        float randY = Random.Range(-4.5f, 4.5f);


        //��
        if (type == 0)
            transform.position = new Vector3(randX, 4.5f, 0);
        //�Ʒ�
        else if (type == 1)
            transform.position = new Vector3(randX, -4.5f, 0);
        //����
        else if (type == 2)
            transform.position = new Vector3(4.5f, randY, 0);
        //����
        else if (type == 3)
            transform.position = new Vector3(-4.5f, randY, 0);


        targetPos = GameObject.FindWithTag("Player").transform; // Player �±׸� ���� ������Ʈ�� targetPos�� ����
        direction = (targetPos.position - transform.position).normalized; //Ÿ�ٰ��� �Ÿ� ��� �� ����ȭ�� ���� ���� ���� direction ����

    }


    /// <summary>
    /// ȭ�� ������ ���� ������Ʈ�� �����ϱ� ���� �Լ�
    /// </summary>
    void DestroyMe()
    {
        if (transform.position.x <= -5 || transform.position.x >= 5 || transform.position.y <= -6 || transform.position.y >= 6)
            Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.isPlay)
            return;

        //�÷��̾�� �浹 �� 
        if (collision.gameObject.CompareTag("Player"))
        {

            GameManager.instance.isPlay = false; //EndGame �Լ��� Invoke�� �ɷ��־� �ﰢ������ ������� �ʱ� ������ isPlay ���� false�� ������ ������ ���������� �����ϱ� ���� ���.
            AudioManager.instance.StopBGM(); // ���� ������  �÷��̾�� �浹�� bgm�� ��� �����ϱ� ���� ���.
          
            collision.gameObject.GetComponent<Player>().Die(); //player ���� ó��. 
            GameManager.instance.EndGame();// ���������� ������ �����ϱ� ���� ���. 
          
            Destroy(gameObject);
        }
    }
}
