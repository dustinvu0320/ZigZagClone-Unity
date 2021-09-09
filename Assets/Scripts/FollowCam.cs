using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Position of character
    public Transform target;
    private Vector3 offset;


    void Awake()
    {
        offset = transform.position - target.position;
    }

    // Function will be called everytime behavior is enabled
    // Normally use for camera to go along with character
    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
