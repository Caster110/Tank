using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform SelfTransform;
    private Vector3 force;
    public float speed;
    private Rigidbody2D rb;

    public GameObject projectile;
    public Transform shotPoint;
    private float timeBtwShots;
    private float startBtwShots;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
                pjtl.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * (speed + 1f);
                timeBtwShots = startBtwShots;
                Debug.Log(radian);
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        SelfTransform.position += force;
        Vector3 position = rb.position;
        rb.position = SelfTransform.position;
        force = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            force += SelfTransform.up * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            force += -SelfTransform.up * Time.deltaTime * (speed - 1f);

        if (Input.GetKey(KeyCode.A))
            SelfTransform.Rotate(0, 0, 2);
        if (Input.GetKey(KeyCode.D))
            SelfTransform.Rotate(0, 0, -2);
    }
}
