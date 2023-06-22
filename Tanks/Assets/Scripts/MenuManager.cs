using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static string i;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void test()
    {
        Debug.Log(i);
    }

    public static void Choice(string number)
    {
        i = number;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public static void Play()
    {
        SceneManager.LoadScene(i);
    }
}
