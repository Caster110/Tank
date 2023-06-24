using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 5f;
    }
    void Update()
    {
        /*RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 10f, whatIsSolid);

        transform.Translate(Vector2.up * speed * Time.deltaTime);*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
