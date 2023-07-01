using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int chosenMap = 1;
    private void Choice(int i)
    {
        chosenMap = i;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
