using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Score : MonoBehaviour
{
    private int scoreNumber;
    [SerializeField] private Text scoreText;

    private static int[] scoreArray = new int[10];

    private void Start()
    {
        scoreNumber = 0;
        SinglePlayerProjectile.EnemyDeath += Increase;

        string recordsFilePath = Path.Combine(Application.dataPath, "StreamingAssets", "Records.txt");

        if (File.Exists(recordsFilePath))
        {
            string[] lines = File.ReadAllLines(recordsFilePath);

            for (int i = 0; i < lines.Length; i++)
            {
                if (int.TryParse(lines[i], out int value))
                    scoreArray[i] = int.Parse(lines[i]);
                else
                    Debug.LogWarning("������ ��� �������� ������ " + (i + 1) + " � ����� " + recordsFilePath);
            }
        }
        else
        {
            Debug.LogWarning("���� " + recordsFilePath + " �� ������");
        }
    }
    public void Increase()
    {
        scoreText.text = (++scoreNumber).ToString();
    }
    public void FinalScore()
    {
        AddScoreArray();
    }

    public void AddScoreArray()
    {
        int index = -1;
        int minIndex = 0;

        for (int i = 0; i < scoreArray.Length; i++)
        {
            if (scoreArray[i] == 0)
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            scoreArray[index] = scoreNumber;
        }
        else
        {
            for (int i = 1; i < scoreArray.Length; i++)
            {
                if (scoreArray[i] < scoreArray[minIndex])
                    minIndex = i;
            }

            if (scoreNumber > scoreArray[minIndex])
                scoreArray[minIndex] = scoreNumber;
        }

        Array.Sort(scoreArray);
        Array.Reverse(scoreArray);

        for (int i = 0; i < scoreArray.Length; i++)
        {
            string key = "Record" + (i + 1);
            PlayerPrefs.SetInt(key, scoreArray[i]);
        }

        PlayerPrefs.Save();
        SaveScoresToFile();
    }

    private void SaveScoresToFile()
    {
        string recordsFilePath = Path.Combine(Application.dataPath, "StreamingAssets", "Records.txt");
        string[] scoreStrings = new string[scoreArray.Length];
        for (int i = 0; i < scoreArray.Length; i++)
            scoreStrings[i] = scoreArray[i].ToString();
        File.WriteAllLines(recordsFilePath, scoreStrings);
    }

    private void OnDestroy()
    {
        SinglePlayerProjectile.EnemyDeath -= Increase;
    }
}