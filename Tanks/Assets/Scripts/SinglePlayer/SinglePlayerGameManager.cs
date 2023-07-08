using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerGameManager : MonoBehaviour
{
    [SerializeField] private GameObject panelDeath;
    [SerializeField] private GameObject panelLive;

    private void Start()
    {
        MainPlayerController.PlayerDeath += OnDefeat;
    }
    public void OnDefeat()
    {
        Time.timeScale = 0f;
        panelDeath.SetActive(true);
        panelLive.SetActive(false);
        MainPlayerController.PlayerDeath -= OnDefeat;
    }
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
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
    }
}
