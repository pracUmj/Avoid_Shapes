using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    

    private AudioSource audioSource;
    public AudioClip[] bgmClip; /* type => menu: 0,  game: 1,2,3 */
    public AudioClip[] sfxClip; /* type => menu: 0,  game: 1,2,3 */



    private void Awake()
    {

        if (instance == null)
            instance = this;

        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

    }


    void Start()
    {
        AudioManager.instance.PlayBgm();
    }



    /// <summary>
    /// ����� ����� ���� �Լ�.
    /// </summary>
    /// <param name="type"></param>
    public void PlayBgm(int type = 0)
    {
        audioSource.clip = bgmClip[type];
        audioSource.Play();
    }



    /// <summary>
    /// ȿ���� ����� ���� �Լ�.
    /// </summary>
    /// <param name="type"></param>
    public void PlaySfx(int type = 0) => audioSource.PlayOneShot(sfxClip[type]);



    /// <summary>
    /// ������� ���߱� ���� �Լ�.
    /// </summary>
    public void StopBGM() => audioSource.Stop();



}
