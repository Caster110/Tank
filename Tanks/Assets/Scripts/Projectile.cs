using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifeTime;
    void Start()
    {
        lifeTime = 5f;
        if (gameObject.name == "ProjectileEnemy(Clone)")
            Destroy(gameObject, lifeTime);
        else
            Destroy(gameObject, (lifeTime + 3f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            if (collision.gameObject.name == "Enemy(Clone)")
                Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.name == "ProjectileBlue(Clone)")
            MainPlayerController.projectileCount--;
        else if(gameObject.name == "ProjectileRed(Clone)")
            SidePlayerController.projectileCount--;
    }
}
