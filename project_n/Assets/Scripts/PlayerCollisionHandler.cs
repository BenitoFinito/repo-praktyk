using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private Movement movementScript;
    private Rigidbody rb;
    public bool handlingCollision = false;
    private CameraClamp cameraClamp;

    void Start()
    {
        movementScript = GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();

        cameraClamp = FindObjectOfType<CameraClamp>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Danger")
        {
            Destroy(collision.gameObject);
        }

        if (!handlingCollision && collision.gameObject.CompareTag("Wall"))
        {
            handlingCollision = true;

            movementScript.enabled = false;

            if (cameraClamp != null)
            {
                cameraClamp.enabled = false;
            }

            rb.useGravity = true;

            FlingPlayer();

            StartCoroutine(HandleCollision());
        }

    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitForSeconds(3);

        rb.useGravity = false;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = new Vector3(-10, 0, -5);
        transform.rotation = Quaternion.identity;

        movementScript.enabled = true;

        if (cameraClamp != null)
        {
            cameraClamp.enabled = true;
        }

        handlingCollision = false;
    }

        private void FlingPlayer()
    {
        Vector3 flingDirection = Random.onUnitSphere;
        flingDirection.y = Mathf.Abs(flingDirection.y);

        float flingForce = 10f;
        rb.AddForce(flingDirection * flingForce, ForceMode.Impulse);
    
        Vector3 torqueAxis = Random.onUnitSphere;

        float torqueForce = 5f;
        rb.AddTorque(torqueAxis * torqueForce, ForceMode.Impulse);
    }

}
