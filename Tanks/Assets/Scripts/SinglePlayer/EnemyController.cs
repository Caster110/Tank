using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    private GameObject target;
    private float speed = 2.5f;
    private Rigidbody2D rigidBody;
    private Vector2 rigidBodyNextPosition;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private Transform centerPoint;
    private Vector3 directionOfTank => shotPoint.position - centerPoint.position;
    private Vector3 directionToPlayer => target.transform.position - centerPoint.position;

    private RaycastHit2D raycastAim;
    private float timerBtwShots;
    private float staticTimeBtwShots = 1.5f;
    private void Start()
    {
        target = GameObject.Find("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.centerOfMass = Vector3.zero;
    }

    private void FixedUpdate()
    {
        timerBtwShots -= Time.fixedDeltaTime;
        raycastAim = Physics2D.Raycast(shotPoint.position, directionOfTank, 50f);
        if (raycastAim.transform != target.transform)
            SelectDirection();
        else if(directionToPlayer.magnitude >= 5f)
            Move();
        if (timerBtwShots <= 0 && raycastAim.transform == target.transform && directionToPlayer.magnitude < 10f)
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
}