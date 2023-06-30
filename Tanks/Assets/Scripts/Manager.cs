using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Map
{
    public static int number = 1;
}
public class Manager : MonoBehaviour
{
    private void Choice(int i)
    {
        Map.number = i;
    }
    private void Exit()
    {
        Application.Quit();
    }

    private void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
