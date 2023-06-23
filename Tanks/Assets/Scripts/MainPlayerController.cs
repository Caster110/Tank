using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform SelfTransform;
    private Vector3 force;
    public float speed;

    public GameObject projectile;
    public Transform shotPoint;
    private float timeBtwShots;
    private float startBtwShots;

    void Start()
    {
        speed = 5f;
        timeBtwShots = 0f;
        startBtwShots = 1f;
    }

    void Update()
    {
        SelfTransform.position += force;
        force = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            force += SelfTransform.up * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            force += -SelfTransform.up * Time.deltaTime * (speed - 1f);

        if (Input.GetKey(KeyCode.A))
            SelfTransform.Rotate(0, 0, 2);
        if (Input.GetKey(KeyCode.D))
            SelfTransform.Rotate(0, 0, -2);

        if (timeBtwShots <= 0)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                Instantiate(projectile, shotPoint.position, Quaternion.Euler(0, 0, 0));
                timeBtwShots = startBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
