using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    protected GameObject[] projectiles;
    [SerializeField] private new Transform camera;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject button;

    public bool redWin;
    public bool blueWin;

    [SerializeField] private Text textRedPoints;
    [SerializeField] private Text textBluePoints;
    private static int valueRedPoints;
    private static int valueBluePoints;

    [SerializeField] private GameObject playerBlue;
    [SerializeField] private GameObject playerRed;
    GameObject bluePlayerOnScene;
    GameObject redPlayerOnScene;

    private static Vector2 blueSpawn;
    private static Vector2 redSpawn;
    public static bool coroutineInProcess = false;
    private int mapNumber = MenuManager.chosenMap;
    private bool isRandomMap = MenuManager.chosenRandomMap;
    protected bool isOnePlayerGame => SceneManager.GetActiveScene().name == "OnePlayerGame";

    public void OnDefeat()
    {
        if(isOnePlayerGame)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            button.SetActive(false);
        }
        else if (!coroutineInProcess)
        {
            StartCoroutine("OnWin");
        }
    }

    public IEnumerator OnWin()
    {
        coroutineInProcess = true;
        yield return new WaitForSeconds(3);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1);

        if (bluePlayerOnScene == null)
            textRedPoints.text = (++valueRedPoints).ToString();
        if (redPlayerOnScene == null)
            textBluePoints.text = (++valueBluePoints).ToString();

        yield return new WaitForSecondsRealtime(2);

        ProjDestroy();
        ChangeMap();

        coroutineInProcess = false;
        StopCoroutine("OnWin");
    }

    private void ProjDestroy()
    {
        Time.timeScale = 1f;
        projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject i in projectiles)
            Destroy(i);
    }

    public void ChangeMap()
    {
        //if (isRandomMap)
        //    mapNumber = Random(1, 5);
        switch (mapNumber)
        {
            case 1:
                camera.position = new Vector3(-49f, 8.5f, -10f);
                redSpawn = new Vector2(-42f, 13f);
                blueSpawn = new Vector2(-56f, 4f);
                break;
            case 2:
                camera.position = new Vector3(-24f, 8.5f, -10f);
                redSpawn = new Vector2(-19f, 11f);
                blueSpawn = new Vector2(-29f, 6f);
                break;
            case 3:
                camera.position = new Vector3(-50f, -7.5f, -10f);
                redSpawn = new Vector2(-43f, -7.5f);
                blueSpawn = new Vector2(-54f, -7.5f);
                break;
            case 4:
                camera.position = new Vector3(-24f, -7.5f, -10f);
                redSpawn = new Vector2(-19f, -20);
                blueSpawn = new Vector2(-29f, -20f);
                break;
            case 5:
                camera.position = new Vector3(-49f, -23.5f, -10f);
                redSpawn = new Vector2(-44f, -20f);
                blueSpawn = new Vector2(-54f, -20f);
                break;
            case 6:
                camera.position = new Vector3(-24f, -23.5f, -10f);
                redSpawn = new Vector2(-19f, -20f);
                blueSpawn = new Vector2(-29f, -20f);
                break;
        }
        Spawn();
    }

    private void Spawn()
    {

        redWin = false;
        blueWin = false;
        if (coroutineInProcess)
        {
            Destroy(redPlayerOnScene);
            Destroy(bluePlayerOnScene);
        }
        redPlayerOnScene = Instantiate(playerRed, redSpawn, playerRed.transform.rotation);
        bluePlayerOnScene = Instantiate(playerBlue, blueSpawn, playerBlue.transform.rotation);
    }
}
