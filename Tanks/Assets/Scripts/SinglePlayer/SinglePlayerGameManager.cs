using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerGameManager : MonoBehaviour
{
    [SerializeField] private GameObject panelDeath;
    [SerializeField] private GameObject panelLive;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text scoreText;

    public void OnDefeat()
    {
        ChangePauseCondition("defeat");
        panelDeath.SetActive(true);
        panelLive.SetActive(false);
        finalScoreText.text += scoreText.text;
    }
    public void LoadMainMenu()
    {
        ChangePauseCondition("exit");
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        ChangePauseCondition("restart");
        SceneManager.LoadScene("OnePlayerGame");
    }
    public void ChangePauseCondition(string description)
    {
        if (description == "restart" || description == "exit" || description == "play")
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
    }
}
