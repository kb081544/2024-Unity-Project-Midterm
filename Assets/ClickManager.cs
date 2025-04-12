using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class ClickManager : MonoBehaviour
{
    public UnityEvent onStartButtonClicked;
    public void SceneChange()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void HowToPlaySceneChange()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }
}
