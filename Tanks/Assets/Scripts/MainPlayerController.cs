using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform SelfTransform;
    public float speed;
    private Rigidbody2D rb;
    private Vector2 rigidPos;

    public GameObject projectile;
    public Transform shotPoint;
    private float timerBtwShots;
    private float timeBtwShots;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
        timeBtwShots = 0.37f;
    }

    void Update()
    {
        if (timerBtwShots <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                float radian = transform.rotation.eulerAngles.z * Mathf.Deg2Rad + Mathf.PI / 2;
                GameObject pjtl = Instantiate(projectile, shotPoint.position, transform.rotation);
                pjtl.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * (speed + 1.5f);
                timerBtwShots = timeBtwShots;
            }
        }
        else
        {
            timerBtwShots -= Time.deltaTime;
        }
    }

void FixedUpdate()
    {
        rigidPos = rb.position;
        Vector2 objPos = SelfTransform.up;

        if (Input.GetKey(KeyCode.W))
            rigidPos += objPos * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            rigidPos -= objPos * Time.deltaTime * (speed - 1f);
        
        if (Input.GetKey(KeyCode.A))
            SelfTransform.Rotate(0, 0, 3f);
        if (Input.GetKey(KeyCode.D))
            SelfTransform.Rotate(0, 0, -3f);

        rb.MovePosition(rigidPos);
    }
}
