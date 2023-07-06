using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject targetPlayer;
    private float speed;
    private Rigidbody2D rigidBody;
    private Vector2 rigidBodyNextPosition;
    private Score score;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private Transform centerPoint;
    private Vector3 directionOfTank => shotPoint.position - centerPoint.position;
    private Vector3 directionToPlayer => targetPlayer.transform.position - centerPoint.position;

    private RaycastHit2D raycastAim;
    private float timerBtwShots;
    private float staticTimeBtwShots;
    private void Start()
    {
        score = GameObject.Find("GameManager").GetComponent<Score>();
        targetPlayer = GameObject.Find("PlayerBlue");
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.centerOfMass = Vector3.zero;
        speed = 2.5f;
        staticTimeBtwShots = 1.5f;
        Move();
    }


    private void FixedUpdate()
    {
        timerBtwShots -= Time.fixedDeltaTime;
        raycastAim = Physics2D.Raycast(shotPoint.position, directionOfTank, 50f);

        if (raycastAim.transform != targetPlayer.transform)
            SelectDirection();
        else if(directionToPlayer.magnitude >= 5f)
            Move();
        if (timerBtwShots <= 0 && raycastAim.transform == targetPlayer.transform && directionToPlayer.magnitude < 10f)
            Shoot();
        rigidBody.angularVelocity = 0f;
        rigidBody.velocity = new Vector2(0, 0);
    }
    private void Move()
    {
        rigidBodyNextPosition = rigidBody.position;
        Vector2 objectNextPosition = transform.up;

        rigidBodyNextPosition += objectNextPosition * Time.deltaTime * speed;

        rigidBody.MovePosition(rigidBodyNextPosition);
    }

    private void Shoot()
    {
        float radian = transform.rotation.eulerAngles.z * Mathf.Deg2Rad + Mathf.PI / 2;
        GameObject projectileObject = Instantiate(projectile, shotPoint.position, transform.rotation);
        projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * (speed + 2.5f);
        timerBtwShots = staticTimeBtwShots;
    }

    private void SelectDirection()
    {
        Vector3 side = Vector3.Cross(directionOfTank, directionToPlayer);
        if (side.z > 0)
            rigidBody.MoveRotation(rigidBody.rotation + 2f);
        else if (side.z < 0)
            rigidBody.MoveRotation(rigidBody.rotation - 2f);
    }

    private void OnDestroy()
    {
        if(score.isActiveAndEnabled)
            score.Increase();
    }
}