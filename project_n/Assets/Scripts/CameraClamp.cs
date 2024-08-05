using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;
    [SerializeField]
    private float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -4f, 0f),
            Mathf.Clamp(targetToFollow.position.y, -11f, 11f),
            transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
