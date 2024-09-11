using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (!GameManager.instance.isPlay)
              return;

        Move();

    }


    /// <summary>
    /// Player의 움직임을 제어하기 위한 함수. 
    /// </summary>
    private void Move()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float x = mousePos.x;
        float y = mousePos.y;

        if (x > 2.7f)
            x = 2.7f;
        if (x < -2.7f)
            x = -2.7f;
        if (y > 4.7f)
            y = 4.7f;
        if (y < -4.7f)
            y = -4.7f;

        transform.position = new Vector3(x, y, 0f);
    }

    /// <summary>
    /// Player 죽음 처리 
    /// </summary>
    public void Die()
    {
        AudioManager.instance.PlaySfx(0);
        anim.SetBool("isDie", true);
    }

}
