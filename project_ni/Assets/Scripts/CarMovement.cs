using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float vMax = 300f;
    private float maxSpeed;
    public float turnSpeed = 50f;
    private Rigidbody rb;

    private float currentSpeed;
    private float moveInput;
    private float turnInput;
    private float timeSinceStart;

    public WheelRotation wheelRotation;
    public float maxSteerAngle = 40f;

    public float timeTo100kmh = 1.7f; 
    public float timeTo200kmh = 3.8f; 
    public float timeTo300kmh = 8.6f; 
    public float timeToMaxSpeed = 10.0f; 

    private CheckpointManager checkpointManager;
    public float speedThreshold = 100f; 
    public float resetDelay = 2.5f;    
    private float resetTimer = 0f;

    public float collisionAngleThreshold = 45f; 

    //private bool isColliding = false;
    //private Collision lastCollision = null;

    void Start()
    {
        maxSpeed = vMax / 3.6f;
        rb = GetComponent<Rigidbody>();
        rb.mass = 2000f;
        timeSinceStart = 0f;
        resetTimer = 0f;

        checkpointManager = CheckpointManager.Instance;

        //IgnoreWheelCollisions();
    }

    void FixedUpdate()
    {
       /* if (isColliding)
        {
            AdjustSpeedBasedOnCollisionAngle();
        }
       */
        GetInput();
        timeSinceStart += Time.fixedDeltaTime;
        currentSpeed = GetCurrentSpeed(timeSinceStart);

        float speedInKmh = currentSpeed * 3.6f;

        Debug.Log("Speed: " + speedInKmh + " km/h, Timer: " + resetTimer);

        if (speedInKmh < speedThreshold)
        {
            resetTimer += Time.deltaTime;

            if (resetTimer >= resetDelay)
            {
                Debug.Log("Triggering vehicle reset...");
                ResetVehiclePosition();
            }
        }
        else
        {
            resetTimer = 0f;
        }

        Vector3 move = transform.forward * currentSpeed * Time.fixedDeltaTime;
        Quaternion turn = Quaternion.Euler(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);

        if (Mathf.Abs(currentSpeed) > 0.01f)
        {
            rb.MovePosition(rb.position + move);
            rb.MoveRotation(rb.rotation * turn);
        }

        if (wheelRotation != null)
        {
            wheelRotation.UpdateWheelRotation(currentSpeed, turnInput, maxSteerAngle);
        }

    }

    private void GetInput()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    private float GetCurrentSpeed(float time)
    {
        float speed100 = 100f / 3.6f;
        float speed200 = 200f / 3.6f;
        float speed300 = 300f / 3.6f;

        if (time <= timeTo100kmh)
        {
            return Mathf.Lerp(0, Mathf.Min(speed100, maxSpeed), time / timeTo100kmh);
        }
        else if (time <= timeTo200kmh)
        {
            float t = (time - timeTo100kmh) / (timeTo200kmh - timeTo100kmh);
            return Mathf.Lerp(Mathf.Min(speed100, maxSpeed), Mathf.Min(speed200, maxSpeed), t);
        }
        else if (time <= timeTo300kmh)
        {
            float t = (time - timeTo200kmh) / (timeTo300kmh - timeTo200kmh);
            return Mathf.Lerp(Mathf.Min(speed200, maxSpeed), Mathf.Min(speed300, maxSpeed), t);
        }
        else
        {
            float t = (time - timeTo300kmh) / (timeToMaxSpeed - timeTo300kmh);
            return Mathf.Lerp(Mathf.Min(speed300, maxSpeed), maxSpeed, t);
        }
    }

    private void ResetVehiclePosition()
    {
        Vector3 resetPosition = checkpointManager.GetLastCheckpointPosition();
        if (resetPosition != Vector3.zero)
        {
            rb.position = resetPosition;
            rb.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("Pojazd zresetowany do pozycji: " + resetPosition);
        }
        resetTimer = 0f;
    }

    /*
    private void IgnoreWheelCollisions()
    {
        Collider[] allColliders = GetComponentsInChildren<Collider>();

        Collider carCollider = GetComponent<BoxCollider>();
        foreach (Collider collider in allColliders)
        {
            if (collider != carCollider && collider.CompareTag("Wheel"))
            {
                Physics.IgnoreCollision(carCollider, collider);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (!collision.collider.CompareTag("Wheel"))
        {
            isColliding = true;
            lastCollision = collision;
            timeSinceStart = 0f;
            AdjustSpeedBasedOnCollisionAngle();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    private void AdjustSpeedBasedOnCollisionAngle()
    {
        if (lastCollision != null)
        {
                currentSpeed = 0;
                turnInput = 0;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
  
        }
    }*/
}
