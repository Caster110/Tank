using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int chosenMap = 0;
    public static bool chosenRandomMap;

    [SerializeField] private Text Record1;
    [SerializeField] private Text Record2;
    [SerializeField] private Text Record3;
    [SerializeField] private Text Record4;
    [SerializeField] private Text Record5;
    [SerializeField] private Text Record6;
    [SerializeField] private Text Record7;
    [SerializeField] private Text Record8;
    [SerializeField] private Text Record9;
    [SerializeField] private Text Record10;

    private void Start()
    {
        string recordsFilePath = Path.Combine("StreamingAssets", "Records.txt");

        if (File.Exists(recordsFilePath))
        {
            string[] lines = File.ReadAllLines(recordsFilePath);

            for (int i = 0; i < lines.Length; i++)
            {
                string key = "Record" + (i + 1);
                int value;

                if (int.TryParse(lines[i], out value))
                    PlayerPrefs.SetInt(key, value);
                else
                    Debug.LogWarning("Ошибка при парсинге строки " + (i + 1) + " в файле " + recordsFilePath);
            }
        }
        else
        {
            Debug.LogWarning("Файл " + recordsFilePath + " не найден");
        }

        TextAsset recordsTextAsset = Resources.Load<TextAsset>("Records");
        if (recordsTextAsset != null)
        {
            Debug.LogWarning("Файл Records.txt найден в папке Resources.");
            string[] lines = recordsTextAsset.text.Split('\n');
        }
        else
        {
            Debug.LogWarning("Файл Records.txt не найден в папке Resources.");
        }
    }

    public void RecordsDisplay()
    {
        Record1.text = "1. " + PlayerPrefs.GetInt("Record1").ToString();
        Record2.text = "2. " + PlayerPrefs.GetInt("Record2").ToString();
        Record3.text = "3. " + PlayerPrefs.GetInt("Record3").ToString();
        Record4.text = "4. " + PlayerPrefs.GetInt("Record4").ToString();
        Record5.text = "5. " + PlayerPrefs.GetInt("Record5").ToString();
        Record6.text = "6. " + PlayerPrefs.GetInt("Record6").ToString();
        Record7.text = "7. " + PlayerPrefs.GetInt("Record7").ToString();
        Record8.text = "8. " + PlayerPrefs.GetInt("Record8").ToString();
        Record9.text = "9. " + PlayerPrefs.GetInt("Record9").ToString();
        Record10.text = "10. " + PlayerPrefs.GetInt("Record10").ToString();
    }

    public void DeleteRecords()
    {
        PlayerPrefs.DeleteAll();

        Record1.text = "1. -";
        Record2.text = "2. -";
        Record3.text = "3. -";
        Record4.text = "4. -";
        Record5.text = "5. -";
        Record6.text = "6. -";
        Record7.text = "7. -";
        Record8.text = "8. -";
        Record9.text = "9. -";
        Record10.text = "10. -";

        string recordsFilePath = Path.Combine("StreamingAssets", "Records.txt");

        if (File.Exists(recordsFilePath))
        {
            string[] lines = File.ReadAllLines(recordsFilePath);
            for (int i = 0; i < lines.Length; i++)
                lines[i] = "0";
            File.WriteAllLines(recordsFilePath, lines);
        }
        else
        {
            Debug.LogWarning("Файл " + recordsFilePath + " не найден");
        }
    }

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
