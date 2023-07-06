using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("OnePlayerGame");
        ChangePauseCondition();
    }
    public void ChangePauseCondition()
    {
        if(Time.timeScale == 0f)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
    }
}
