using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
public class MainPlayerController : MonoBehaviour
{
    private float speed = 4.5f;
    private Rigidbody2D rigidBody;
    private Vector2 rigidBodyNextPosition;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    private float timerBtwShots;
    private float staticTimeBtwShots = 0.4f;
    public static int projectileCount;
    private int maxProjectileCount = 8;

    public UnityEvent PlayerDeath;

    void Start()
    {
        projectileCount = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.centerOfMass = Vector3.zero;
    }
    void Update()
    {
        if (timerBtwShots <= 0)
        {
            if (Input.GetKey(KeyCode.V) && projectileCount < maxProjectileCount && Time.timeScale != 0f)
            {
                float rotationToRadian = transform.rotation.eulerAngles.z * Mathf.Deg2Rad + Mathf.PI / 2;
                GameObject projectileObject = Instantiate(projectile, shotPoint.position, transform.rotation);
                projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(rotationToRadian), Mathf.Sin(rotationToRadian)) * (speed + 2f);
                timerBtwShots = staticTimeBtwShots;
                projectileCount++;
            }
        }
        else
        {
            timerBtwShots -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        rigidBodyNextPosition = rigidBody.position;
        Vector2 objectNextPosition = transform.up;

        if (Input.GetKey(KeyCode.W))
            rigidBodyNextPosition += objectNextPosition * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            rigidBodyNextPosition -= objectNextPosition * Time.deltaTime * (speed - 1f);

        rigidBody.MovePosition(rigidBodyNextPosition);

        if (Input.GetKey(KeyCode.A))
            rigidBody.MoveRotation(rigidBody.rotation + 3f);
        if (Input.GetKey(KeyCode.D))
            rigidBody.MoveRotation(rigidBody.rotation - 3f);
        rigidBody.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
            PlayerDeath?.Invoke();
    }
}
