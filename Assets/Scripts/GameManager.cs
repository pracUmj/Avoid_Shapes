using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] shapes;

    //UI
    public GameObject endPanel;
    public Text timeText;
    public Text currentScoreText;
    public Text highScoreText;

    //Settings
    private float time = 0f;
    private int level = 0;
    private string key = "HighScore";

    public bool isPlay = true;



    private void Awake()
    {


        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60; 
    }




    void Start()
    {
        Init();
        InvokeRepeating("MakeShape", 0, 0.5f);
    }


    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            HighlightScore();
        }
    }

    /// <summary>
    /// 게임 초기값 세팅 
    /// </summary>
    private void Init()
    {
        Time.timeScale = 1.0f;
        isPlay = true;
        AudioManager.instance.PlayBgm(1);
    }


    /// <summary>
    /// 점수를 시각화 하고 최고 점수 달성 시 강조 효과를 주기 위한 함수. 
    /// </summary>
    void HighlightScore()
    {
        if (time > PlayerPrefs.GetFloat(key))
        {
            timeText.color = new Color(240 / 255f, 0f, 0f, 1f); 
            timeText.text = "Best " + time.ToString("N2");
        }

        else
        {
            timeText.text = time.ToString("N2");
        }
    }


    /// <summary>
    /// 도형 발사체 생성을 위한 함수 
    /// </summary>
    void MakeShape()
    {
        Instantiate(shapes[Random.Range(0, 3)]);


        // level 0, 30% 확률로 도형 추가 생성 
        if (time >0f && time <= 10.0f)
        {
            int x = Random.Range(0, 10);
            if (x >= 0 && x < 3)          
                Instantiate(shapes[Random.Range(0, 3)]);
        }

        // level 1, 도형 추가 생성, 50% 확률로 한번 더 생성
        else if (time > 15.0f && time <= 20.0f)
        {

            // 다음 단계로 처음 변경 될 시 level값을 올려주고 단계에 맞는 배경음 재생처리
            if (level == 0)
            {
                level = 1;
                AudioManager.instance.StopBGM();
                AudioManager.instance.PlayBgm(2);
            }


            Instantiate(shapes[Random.Range(0, 3)]);

            int x = Random.Range(0, 10);
            if (x >= 0 && x < 5)
            {
                Instantiate(shapes[Random.Range(0, 3)]);
            }
        }

        // level 2 , 큰 도형 추가 생성 , 70% 확률로  기본 도형 추가 생성
        else if (time > 30.0f)
        {

            if (level == 1)
            {
                level = 2;
                AudioManager.instance.StopBGM();
                AudioManager.instance.PlayBgm(3);
            }

            float size = Random.Range(1f, 1.5f);
            GameObject bigShape = Instantiate(shapes[Random.Range(0, 3)]);
            bigShape.transform.localScale = new Vector3(size, size, 1f);

            int x = Random.Range(0, 10);
            if (x >= 0 && x < 7)
            {
                Instantiate(shapes[Random.Range(0, 3)]);
            }
        }



    }

    public void EndGame()
    {
        Invoke("InvokeEndGame", 1f);
    }


    /// <summary>
    /// 게임오버 됐을 때 EndPanel UI를 시각화 및 점수 기록  
    /// </summary>
    private void InvokeEndGame()
    {
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetFloat(key, time); 
        
        else
        {
            float highScore = PlayerPrefs.GetFloat(key); 
            if (highScore < time)
                PlayerPrefs.SetFloat(key, time);


        }

  
        currentScoreText.text = time.ToString("N2");
        highScoreText.text = PlayerPrefs.GetFloat(key).ToString("N2");


        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }

}
