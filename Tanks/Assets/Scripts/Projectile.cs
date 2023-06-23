using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public LayerMask whatIsSolid;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 6f;
        lifeTime = 5f;
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 10f, whatIsSolid);

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = collision.GetContact(0).normal;

            // Отражаем вектор скорости от стены
            Vector2 reflectedVelocity = Vector2.Reflect(rb.velocity, normal);

            // Применяем отраженную скорость
            rb.velocity = reflectedVelocity.normalized * rb.velocity.magnitude;
        }
    }
}
