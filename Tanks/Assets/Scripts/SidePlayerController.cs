using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlayerController : MonoBehaviour
{
    private Transform SelfTransform;
    private float speed;
    private Rigidbody2D rb;
    private Vector2 rigidPos;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    private float timerBtwShots;
    private float staticTimeBtwShots;
    public static int projectileCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SelfTransform = GetComponent<Transform>();
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
                projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * (speed + 1.5f);
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
        rigidPos = rb.position;
        Vector2 objPos = SelfTransform.up;

        if (Input.GetKey(KeyCode.UpArrow))
            rigidPos += objPos * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.DownArrow))
            rigidPos -= objPos * Time.deltaTime * (speed - 1f);
        
        if (Input.GetKey(KeyCode.LeftArrow))
            SelfTransform.Rotate(0, 0, 3f);
        if (Input.GetKey(KeyCode.RightArrow))
            SelfTransform.Rotate(0, 0, -3f);

        rb.MovePosition(rigidPos);
    }
}
