using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 modifiedPlayerPosition;
    private bool cameraInRange;

    private void Start()
    {
        MainPlayerController.PlayerDeath += StopFollow;
    }
    private void Update()
    {
            cameraInRange = player.position.y < 10.5f && player.position.y > -10.5f;
        if (cameraInRange)
        {
            modifiedPlayerPosition = new Vector3(0, player.position.y, -10);
            transform.position = modifiedPlayerPosition;
        }
    }

    private void StopFollow ()
    {
        MainPlayerController.PlayerDeath -= StopFollow;
        enabled = false;
    }
}
