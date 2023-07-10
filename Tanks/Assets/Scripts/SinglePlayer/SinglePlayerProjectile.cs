using UnityEngine;
using UnityEngine.Events;

public class SinglePlayerProjectile : MonoBehaviour
{
    private float lifeTime = 6.5f;
    private bool playerIsOwner => gameObject.name == "ProjectileWhite(Clone)";

    public static event UnityAction EnemyDeath;
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            if (collision.gameObject.name == "Enemy(Clone)")
                EnemyDeath?.Invoke();
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
