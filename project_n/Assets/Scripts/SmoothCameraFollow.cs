using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(10f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 vel = Vector3.zero;

    [SerializeField]
    private Transform target;

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, smoothTime);
    }
}
