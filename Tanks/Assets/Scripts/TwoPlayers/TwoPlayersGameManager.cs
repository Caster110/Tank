using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPlayersGameManager : MonoBehaviour
{
    protected GameObject[] projectiles;
    [SerializeField] private new Transform camera;

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

    public void ChangePauseCondition(string description)
    {
        if (description == "restart" || description == "exit" || description == "play")
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
    }

    public void Restart()
    {
        ChangePauseCondition("restart");
        SceneManager.LoadScene("TwoPlayersGame");
    }

    public void LoadMainMenu()
    {
        ChangePauseCondition("exit");
        SceneManager.LoadScene("Menu");
    }

    public IEnumerator OnWin()
    {
        coroutineInProcess = true;
        yield return new WaitForSeconds(3);
        ChangePauseCondition("win");
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
        switch (mapNumber)
        {
            case 1:
            default:
                camera.position = new Vector3(-49f, 8.5f, -10f);
                redSpawn = new Vector2(-43f, 10f);
                blueSpawn = new Vector2(-55f, 7f);
                break;
            case 2:
                camera.position = new Vector3(-24f, 8.5f, -10f);
                redSpawn = new Vector2(-18f, 12f);
                blueSpawn = new Vector2(-30f, 5f);
                break;
            case 3:
                camera.position = new Vector3(-50f, -7.5f, -10f);
                redSpawn = new Vector2(-41f, -11f);
                blueSpawn = new Vector2(-57f, -4f);
                break;
            case 4:
                camera.position = new Vector3(-24f, -7.5f, -10f);
                redSpawn = new Vector2(-15f, -7.5f);
                blueSpawn = new Vector2(-33f, -7.5f);
                break;
            case 5:
                camera.position = new Vector3(-49f, -23.5f, -10f);
                redSpawn = new Vector2(-41f, -23.5f);
                blueSpawn = new Vector2(-57f, -23.5f);
                break;
            case 6:
                camera.position = new Vector3(-24f, -23.5f, -10f);
                redSpawn = new Vector2(-15f, -23.5f);
                blueSpawn = new Vector2(-33f, -23.5f);
                break;
        }
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        if (coroutineInProcess)
        {
            Destroy(redPlayerOnScene);
            Destroy(bluePlayerOnScene);
        }
        redPlayerOnScene = Instantiate(playerRed, redSpawn, playerRed.transform.rotation);
        bluePlayerOnScene = Instantiate(playerBlue, blueSpawn, playerBlue.transform.rotation);
    }
}
