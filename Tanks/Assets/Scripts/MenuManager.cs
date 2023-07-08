using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int chosenMap = 0;
    public static bool chosenRandomMap;
    public void Choice(int i)
    {
        chosenMap = i;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Play(bool isSinglePlayer)
    {
        if (isSinglePlayer)
            SceneManager.LoadScene("OnePlayerGame");
        else if(chosenMap != 0)
            SceneManager.LoadScene("TwoPlayersGame");
    }
}
