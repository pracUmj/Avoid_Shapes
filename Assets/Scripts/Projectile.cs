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
        transform.position += direction * speed; // 해당 방향으로 속도에 맞춰 이동
        DestroyMe();

    }



    /// <summary>
    /// 생성 시 초기값 설정을 위한 함수.
    /// </summary>
    void Init()
    {

  
        //화면 밖 4개의 영역 중 하나를 무작위 하게 설정
        int type = Random.Range(0, 4);
        //x좌표, y좌표 난수 값을 변수에 담아두기
        float randX = Random.Range(-4.5f, 4.5f);
        float randY = Random.Range(-4.5f, 4.5f);


        //위
        if (type == 0)
            transform.position = new Vector3(randX, 4.5f, 0);
        //아래
        else if (type == 1)
            transform.position = new Vector3(randX, -4.5f, 0);
        //우측
        else if (type == 2)
            transform.position = new Vector3(4.5f, randY, 0);
        //좌측
        else if (type == 3)
            transform.position = new Vector3(-4.5f, randY, 0);


        targetPos = GameObject.FindWithTag("Player").transform; // Player 태그를 가진 오브젝트를 targetPos에 저장
        direction = (targetPos.position - transform.position).normalized; //타겟과의 거리 계산 후 정규화를 통해 방향 값을 direction 저장

    }


    /// <summary>
    /// 화면 밖으로 나간 오브젝트를 삭제하기 위한 함수
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

        //플레이어와 충돌 시 
        if (collision.gameObject.CompareTag("Player"))
        {

            GameManager.instance.isPlay = false; //EndGame 함수는 Invoke가 걸려있어 즉각적으로 실행되지 않기 때문에 isPlay 값을 false로 변경해 게임을 일차적으로 종료하기 위해 사용.
            AudioManager.instance.StopBGM(); // 같은 이유로  플레이어와 충돌시 bgm을 즉시 종료하기 위해 사용.
          
            collision.gameObject.GetComponent<Player>().Die(); //player 죽음 처리. 
            GameManager.instance.EndGame();// 최종적으로 게임을 종료하기 위해 사용. 
          
            Destroy(gameObject);
        }
    }
}
