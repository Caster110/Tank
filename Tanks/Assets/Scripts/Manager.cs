using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Map
{
    public static int number;
}
public class Manager : MonoBehaviour
{
    public void Choice(int i)
    {
        Map.number = i;
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
