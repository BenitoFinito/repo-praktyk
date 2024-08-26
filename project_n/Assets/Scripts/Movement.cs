using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float maxRotationAngle = 60f;

    private Rigidbody rb;
    private float upTime = 0f;
    private float downTime = 0f;
    private Quaternion targetRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(0, verticalInput * speed);

        float clampedY = Mathf.Clamp(transform.position.y, -60f, 60f);
        transform.position = new Vector2(transform.position.x, clampedY);

        if (rb.velocity.y > 0)
        {

            upTime += Time.fixedDeltaTime;
            downTime = 0f;

            float targetAngle = Mathf.Clamp(upTime * maxRotationAngle, 0, maxRotationAngle);
            targetRotation = Quaternion.Euler(0, 0, targetAngle);
        }
        else if (rb.velocity.y < 0)
        {

            downTime += Time.fixedDeltaTime;
            upTime = 0f;
            float targetAngle = Mathf.Clamp(downTime * maxRotationAngle, 0, maxRotationAngle);
            targetRotation = Quaternion.Euler(0, 0, -targetAngle);
        }
        else
        {
            upTime = 0f;
            downTime = 0f;
            targetRotation = Quaternion.Euler(0, 0, 0);
        }

}

}
