using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 modifiedPlayerPosition;
    private bool cameraInRange;

    private void Update()
    {
        cameraInRange = player.position.y < 10.5f && player.position.y > -10.5f;
        if (cameraInRange)
        {
            modifiedPlayerPosition = new Vector3(0, player.position.y, -10);
            transform.position = modifiedPlayerPosition;
        }
    }

    public void StopFollow()
    {
        enabled = false;
    }
}
