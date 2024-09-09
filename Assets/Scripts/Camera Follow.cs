using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    private Vector3 cameraPosition;

    void FixedUpdate()
    {
        if (target != null)
        {
            cameraPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = cameraPosition;
        }
    }
}
