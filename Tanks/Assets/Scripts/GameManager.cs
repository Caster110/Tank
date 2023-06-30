using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject[] projectiles;
    private new Transform camera;

    public static bool redWin;
    public static bool blueWin;

    private static Text textRedPoints;
    private static Text textBluePoints;
    private static int valueRedPoints;
    private static int valueBluePoints;

    public GameObject playerBlue;
    public GameObject playerRed;

    private Vector2 blueSpawn;
    private Vector2 redSpawn;
    public bool coroutineInProcess;

    public void Start()
    {
        textBluePoints = GameObject.Find("BluePoints").GetComponent<Text>();
        textRedPoints = GameObject.Find("RedPoints").GetComponent<Text>();

        coroutineInProcess = false;

        valueBluePoints = 0;
        valueRedPoints = 0;
        
        camera = GameObject.Find("Main Camera").GetComponent<Transform>();

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
        coroutineInProcess = false;
        int map = Map.number;
        //if (Map.isRandom == true)
        //    map = Random(1, 5);
        switch (map)
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
        Destroy(GameObject.Find("PlayerRed(Clone)"));
        Destroy(GameObject.Find("PlayerBlue(Clone)"));
        Instantiate(playerRed, redSpawn, playerRed.transform.rotation);
        Instantiate(playerBlue, blueSpawn, playerBlue.transform.rotation);
    }
}
