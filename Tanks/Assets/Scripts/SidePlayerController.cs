using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlayerController : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rigidBody;
    private Vector2 rigidBodyNextPosition;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    private float timerBtwShots;
    private float staticTimeBtwShots;
    public static int projectileCount;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        speed = 4.5f;
        staticTimeBtwShots = 0.38f;
    }

    void Update()
    {
        if (timerBtwShots <= 0)
        {
            if (Input.GetKey(KeyCode.Slash) && projectileCount <= 5 && Time.timeScale != 0f)
            {
                float radian = transform.rotation.eulerAngles.z * Mathf.Deg2Rad + Mathf.PI / 2;
                GameObject projectileObject = Instantiate(projectile, shotPoint.position, transform.rotation);
                projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * (speed + 2f);
                timerBtwShots = staticTimeBtwShots;
                projectileCount++;
            }
        }
        else
        {
            timerBtwShots -= Time.deltaTime;
        }
    }
    public void OnDestroy()
    {
        GameManager.blueWin = true;
    }
    void FixedUpdate()
    {
        rigidBodyNextPosition = rigidBody.position;
        Vector2 objectNextPosition = transform.up;

        if (Input.GetKey(KeyCode.UpArrow))
            rigidBodyNextPosition += objectNextPosition * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.DownArrow))
            rigidBodyNextPosition -= objectNextPosition * Time.deltaTime * (speed - 1f);
        
        rigidBody.MovePosition(rigidBodyNextPosition);

        if (Input.GetKey(KeyCode.LeftArrow))
            rigidBody.MoveRotation(rigidBody.rotation + 3f);
        if (Input.GetKey(KeyCode.RightArrow))
            rigidBody.MoveRotation(rigidBody.rotation -3f);
        rigidBody.angularVelocity = 0f;
    }
}
