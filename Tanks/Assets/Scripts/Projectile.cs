using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private MapManager mapManager;
    public float lifeTime;
    void Start()
    {
        lifeTime = 7f;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
            mapManager.StartCoroutine("OnWin");
            Destroy(gameObject);
        }
    }
}
