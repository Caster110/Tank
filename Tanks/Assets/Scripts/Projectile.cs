using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private static GameManager manager;
    private float lifeTime;
    void Start()
    {
        lifeTime = 7f;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);

            if (GameObject.FindGameObjectsWithTag("Player").Length == 2)
            {
                manager = GameObject.Find("GameManager").GetComponent<GameManager>();
                manager.StartCoroutine("OnWin");
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.name == "ProjectileBlue(Clone)")
            MainPlayerController.projectileCount--;
        else
            SidePlayerController.projectileCount--;
    }
}
