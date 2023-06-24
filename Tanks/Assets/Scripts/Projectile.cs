using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        lifeTime = 5f;
        Destroy(gameObject, lifeTime);
    }
    void Update()
    {
        /*RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 10f, whatIsSolid);

        transform.Translate(Vector2.up * speed * Time.deltaTime);*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
