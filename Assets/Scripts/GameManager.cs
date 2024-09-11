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
    /// ���� �ʱⰪ ���� 
    /// </summary>
    private void Init()
    {
        Time.timeScale = 1.0f;
        isPlay = true;
        AudioManager.instance.PlayBgm(1);
    }


    /// <summary>
    /// ������ �ð�ȭ �ϰ� �ְ� ���� �޼� �� ���� ȿ���� �ֱ� ���� �Լ�. 
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
    /// ���� �߻�ü ������ ���� �Լ� 
    /// </summary>
    void MakeShape()
    {
        Instantiate(shapes[Random.Range(0, 3)]);


        // level 0, 30% Ȯ���� ���� �߰� ���� 
        if (time >0f && time <= 10.0f)
        {
            int x = Random.Range(0, 10);
            if (x >= 0 && x < 3)          
                Instantiate(shapes[Random.Range(0, 3)]);
        }

        // level 1, ���� �߰� ����, 50% Ȯ���� �ѹ� �� ����
        else if (time > 15.0f && time <= 20.0f)
        {

            // ���� �ܰ�� ó�� ���� �� �� level���� �÷��ְ� �ܰ迡 �´� ����� ���ó��
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

        // level 2 , ū ���� �߰� ���� , 70% Ȯ����  �⺻ ���� �߰� ����
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
    /// ���ӿ��� ���� �� EndPanel UI�� �ð�ȭ �� ���� ���  
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
