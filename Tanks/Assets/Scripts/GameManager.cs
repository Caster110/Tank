using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameObject[] projectiles;
    private static new Transform camera;

    public static bool redWin;
    public static bool blueWin;

    private static Text textRedPoints;
    private static Text textBluePoints;
    private static int valueRedPoints;
    private static int valueBluePoints;

    private static GameObject playerBlue;
    private static GameObject playerRed;
    GameObject prevBluePlayerMemory;
    GameObject prevRedPlayerMemory;

    private static Vector2 blueSpawn;
    private static Vector2 redSpawn;
    public static bool coroutineInProcess = false;
    private int mapNumber = MenuManager.chosenMap;
    private bool isRandomMap = MenuManager.chosenRandomMap;


    private void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Transform>();

        textRedPoints = GameObject.Find("RedPoints").GetComponent<Text>();
        textBluePoints = GameObject.Find("BluePoints").GetComponent<Text>();

        playerBlue = Resources.Load<GameObject>("PlayerBlue");
        playerRed = Resources.Load<GameObject>("PlayerRed");
    }
    public IEnumerator OnWin()
    {
        coroutineInProcess = true;
        yield return new WaitForSeconds(3);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1);

        if (GameObject.Find("PlayerBlue(Clone)") == null)
        {
            valueRedPoints++;
            textRedPoints.text = valueRedPoints.ToString();
        }
        if (GameObject.Find("PlayerRed(Clone)") == null)
        {
            valueBluePoints++;
            textBluePoints.text = valueBluePoints.ToString();
        }

        yield return new WaitForSecondsRealtime(2);

        ProjDestroy();
        ChangeMap();

        redWin = false;
        blueWin = false;

        coroutineInProcess = false;
        StopCoroutine("OnWin");
    }

    private static void ProjDestroy()
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

                redSpawn = new Vector2(-44f, 11f);
                blueSpawn = new Vector2(-54f, 6f);

                Spawn();
                break;
            case 2:
                camera.position = new Vector3(-24f, 8.5f, -10f);

                redSpawn = new Vector2(-19f, 11f);
                blueSpawn = new Vector2(-29f, 6f);

                Spawn();
                break;
            case 3:
                camera.position = new Vector3(-49f, -7.5f, -10f);

                redSpawn = new Vector2(-44f, -7.5f);
                blueSpawn = new Vector2(-54f, -7.5f);

                Spawn();
                break;
            case 4:
                camera.position = new Vector3(-24f, -7.5f, -10f);

                redSpawn = new Vector2(-19f, -7.5f);
                blueSpawn = new Vector2(-29f, -7.5f);

                Spawn();
                break;

        }
    }

    private void Spawn()
    {
        if (coroutineInProcess)
        {
            Destroy(prevRedPlayerMemory);
            Destroy(prevBluePlayerMemory);
        }
        GameObject redPlayerOnScene = Instantiate(playerRed, redSpawn, playerRed.transform.rotation);
        GameObject bluePlayerOnScene = Instantiate(playerBlue, blueSpawn, playerBlue.transform.rotation);
        prevBluePlayerMemory = bluePlayerOnScene;
        prevRedPlayerMemory = redPlayerOnScene;
    }
}
