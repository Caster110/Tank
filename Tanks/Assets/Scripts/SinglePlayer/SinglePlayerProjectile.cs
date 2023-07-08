using System.Collections;
using UnityEngine;

public class SinglePlayerProjectile : MonoBehaviour
{
    private float lifeTime = 5f;
    private bool playerIsOwner => gameObject.name == "ProjectileWhite(Clone)";
    void Start()
    {
        if (playerIsOwner)
            Destroy(gameObject, (lifeTime + 3f));
        else
            Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (playerIsOwner)
            MainPlayerController.projectileCount--;
    }
}
