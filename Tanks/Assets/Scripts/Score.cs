using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int scoreNumber;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text finalScoreText;

    private void Start()
    {
        scoreNumber = 0;
    }
    public void Increase()
    {
        scoreText.text = (++scoreNumber).ToString();
    }
    public void OnDefeat()
    {
        finalScoreText.text += scoreNumber.ToString();
    }
}
