using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform SelfTransform;
    private Vector2 force;
    public float speed;
    private Rigidbody2D rb;

    public GameObject projectile;
    public Transform shotPoint;
    private float timeBtwShots;
    private float startBtwShots;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 position = rb.position;
        speed = 5f;
        timeBtwShots = 0f;
        startBtwShots = 0.5f;
    }

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                float radian = transform.rotation.eulerAngles.z * Mathf.Deg2Rad + Mathf.PI/2;
                GameObject pjtl = Instantiate(projectile, shotPoint.position, transform.rotation);
                pjtl.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * (speed + 1.5f);
                timeBtwShots = startBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 rigid = rb.position;
        Vector2 objPos = SelfTransform.up;

        if (Input.GetKey(KeyCode.W))
            rigid += objPos * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            rigid += -objPos * Time.deltaTime * (speed - 1f);
        rb.MovePosition(rigid);

        if (Input.GetKey(KeyCode.A))
            SelfTransform.Rotate(0, 0, 2.5f);
        if (Input.GetKey(KeyCode.D))
            SelfTransform.Rotate(0, 0, -2.5f);
    }
}
