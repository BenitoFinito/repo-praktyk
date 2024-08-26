using System.Collections;
using UnityEngine;

public class PlayerDummySpawner : MonoBehaviour
{
    public GameObject player;
    private PlayerCollisionHandler playerCollisionHandler;
    private bool stop = false;
    private Vector3 pozycja;
    private Rigidbody rb;

    void Start()
    {

        rb = GetComponent<Rigidbody>();

        if (player != null)
        {
            playerCollisionHandler = player.GetComponent<PlayerCollisionHandler>();
        }

    }

    void Update()
    {
        if (!stop && playerCollisionHandler != null && playerCollisionHandler.handlingCollision)
        {
            rb.useGravity = true;

            pozycja = player.transform.position;
            transform.position = pozycja;

            Vector3 flingDirection = Random.onUnitSphere;
            flingDirection.y = Mathf.Abs(flingDirection.y);

            float flingForce = 10f;
            rb.AddForce(flingDirection * flingForce, ForceMode.Impulse);
    
            Vector3 torqueAxis = Random.onUnitSphere;

            float torqueForce = 5f;
            rb.AddTorque(torqueAxis * torqueForce, ForceMode.Impulse);

            stop = true;
        }

        if(!playerCollisionHandler.handlingCollision){
            stop = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(0, 0, 20);
        }
    }
}
