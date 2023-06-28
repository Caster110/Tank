using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;

public class MapManager : MonoBehaviour
{
    public new Transform camera;
    void Start()
    {
        Time.timeScale = 0f;
    }

    IEnumerator OnWin()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3);
        ChangeMap();
    }

    public void ChangeMap()
    {
        while (GameObject.FindGameObjectsWithTag("Projectile") != null)
            Destroy(GameObject.FindGameObjectWithTag("Projectile"));
        Time.timeScale = 1.0f;
        switch(Map.number)
        {
            case 1:
                camera.position = new Vector3(-24f, 8.5f, -10f);
                break;
        }
    }
}
