using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void LoadMainScene() {
        AudioManager.instance.StopBGM();
        SceneManager.LoadScene("MainScene");
    } 
 




}
