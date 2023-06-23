using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public new Transform camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMap()
    {
        switch(Map.number)
        {
            case 1:
                camera.position = new Vector3(-24f, 8.32f, -10f);
                break;
        }
    }
}
