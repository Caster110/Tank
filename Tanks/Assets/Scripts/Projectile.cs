using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameManager manager;
    private float lifeTime = 7f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);

            if (!GameManager.coroutineInProcess)
                manager.StartCoroutine("OnWin");

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
