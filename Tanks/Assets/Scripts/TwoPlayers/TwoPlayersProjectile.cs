using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayersProjectile : MonoBehaviour
{
    private TwoPlayersGameManager manager;
    private float lifeTime = 8f;
    private bool ownerIsBlue => gameObject.name == "ProjectileBlue(Clone)";
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            if (TwoPlayersGameManager.coroutineInProcess == false)
            {
                manager = GameObject.Find("GameManager").GetComponent<TwoPlayersGameManager>();
                manager.StartCoroutine("OnWin");
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (ownerIsBlue)
            BluePlayerController.projectileCount--;
        else
            RedPlayerController.projectileCount--;
    }
}
