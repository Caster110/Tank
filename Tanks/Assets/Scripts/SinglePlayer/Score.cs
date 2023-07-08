using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    private int scoreNumber;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text finalScoreText;

    private void Start()
    {
        scoreNumber = 0;
        EnemyController.EnemyDeath += Increase;
        MainPlayerController.PlayerDeath += FinalScore;
    }
    public void Increase()
    {
        scoreText.text = (++scoreNumber).ToString();
    }
    public void FinalScore()
    {
        finalScoreText.text += scoreNumber.ToString();
        EnemyController.EnemyDeath -= Increase;
        MainPlayerController.PlayerDeath -= FinalScore;
    }
}
