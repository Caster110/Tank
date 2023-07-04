using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject targetPlayer;
    private Vector3 directionOfTank;
    private Vector3 directionToPlayer;
    private float speed;
    private Rigidbody2D rigidBody;
    private Vector2 rigidBodyNextPosition;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private Transform centerPoint;

    private RaycastHit2D raycastAim;
    private float timerBtwShots;
    private float staticTimeBtwShots;
    public void Start()
    {
        targetPlayer = GameObject.Find("PlayerBlue");
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.centerOfMass = Vector3.zero;
        speed = 4f;
        staticTimeBtwShots = 0.6f;
    }

    public void Update()
    {
        timerBtwShots -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        SelectDirection();
        raycastAim = Physics2D.Raycast(shotPoint.position, directionOfTank, 5f);
        if (directionToPlayer.magnitude > 6f)
            Move();
        else if (timerBtwShots <= 0 && raycastAim == targetPlayer)
            Shoot();
    }
    private void Move()
    {
        rigidBodyNextPosition = rigidBody.position;
        Vector2 objectNextPosition = transform.up;

        rigidBodyNextPosition += objectNextPosition * Time.deltaTime * speed;

        rigidBody.MovePosition(rigidBodyNextPosition);
        rigidBody.angularVelocity = 0f;
    }

    private void Shoot ()
    {
        float radian = transform.rotation.eulerAngles.z * Mathf.Deg2Rad + Mathf.PI / 2;
        GameObject projectileObject = Instantiate(projectile, shotPoint.position, transform.rotation);
        projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * (speed + 2f);
        timerBtwShots = staticTimeBtwShots;
    }

    private void SelectDirection()
    {
        targetPlayer = GameObject.Find("PlayerBlue");
        directionOfTank = shotPoint.position - centerPoint.position;
        directionToPlayer = targetPlayer.transform.position - shotPoint.position;
        if (raycastAim != targetPlayer)
        {
            RaycastHit2D raycastToPlayer = Physics2D.Raycast(shotPoint.position, directionToPlayer, 5f);
            Vector3 side = Vector3.Cross(directionOfTank, directionToPlayer);
            if(Vector3.SignedAngle(directionOfTank, directionToPlayer, Vector3.forward) > 0f)
                rigidBody.MoveRotation(rigidBody.rotation + 2f);
            else
                rigidBody.MoveRotation(rigidBody.rotation - 2f);
        }
    }
}