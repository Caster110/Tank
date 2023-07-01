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
    GameObject tempBlue;
    GameObject tempRed;

    private static Vector2 blueSpawn;
    private static Vector2 redSpawn;
    public static bool coroutineInProcess = false;


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

        int mapNumber = MenuManager.chosenMap;
        //if (Map.isRandom == true)
        //    map = Random(1, 5);
        switch (mapNumber)
        {
            case 1:
                camera.position = new Vector3(-24f, 8.5f, -10f);

                redSpawn = new Vector2(-19f, 11f);
                blueSpawn = new Vector2(-29f, 6f);

                Spawn();
                break;
        }
    }

    private void Spawn()
    {
        if (coroutineInProcess)
        {
            Destroy(tempRed);
            Destroy(tempBlue);
        }
        GameObject redPlayerOnScene = Instantiate(playerRed, redSpawn, playerRed.transform.rotation);
        GameObject bluePlayerOnScene = Instantiate(playerBlue, blueSpawn, playerBlue.transform.rotation);
        tempBlue = bluePlayerOnScene;
        tempRed = redPlayerOnScene;
    }
}
