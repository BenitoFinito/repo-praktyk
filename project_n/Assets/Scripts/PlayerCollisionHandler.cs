using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private Movement movementScript;
    private Rigidbody rb;
    public bool handlingCollision = false;
    private CameraClamp cameraClamp;
    private Collider playerCollider;
    private Renderer[] renderers;
    public float blinkInterval = 0.5f;

    public PointsHUD pointHUD;

    /*public GameObject prefab;        // The prefab to spawn
    public float flingForce = 10f;  // The force applied to fling the prefabs downwards
    private Vector2 position;
    private float positionY;*/

    void Start()
    {
        //movementScript = GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();

        //rb.constraints = RigidbodyConstraints.FreezeRotationZ;

        renderers = GetComponentsInChildren<Renderer>();

        //cameraClamp = FindObjectOfType<CameraClamp>();
        playerCollider = GetComponent<Collider>();

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

            pointHUD.Points -= 20;

            //movementScript.enabled = false;

            //if (cameraClamp != null)
            //{
            //    cameraClamp.enabled = false;
            //}

            //rb.useGravity = true;

            playerCollider.enabled = false;

            //FlingPlayer();

            /*for (int i = 0; i < 10; i++)
            {
                // Instantiate the prefab at the spawn position with no rotation
                positionY = transform.position.y - 3;
                position = new Vector2(transform.position.x, positionY);
                GameObject spawnedObject = Instantiate(prefab, position, Quaternion.identity);

                // Apply a downward force to fling the object downwards
                Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(Vector2.down * flingForce);
                }

                // Destroy the prefab after a certain time
                Destroy(spawnedObject, 5);
            }*/

            StartCoroutine(HandleCollision());

        }

    }


    private IEnumerator HandleCollision()
    {

        for (int i = 0; i < 6; i++){

                foreach (Renderer renderer in renderers){

                    renderer.enabled = !renderer.enabled;

                    rb.angularVelocity = Vector3.zero;
                    transform.rotation = Quaternion.identity;
            
                }

                yield return new WaitForSeconds(blinkInterval);

            }

        //rb.useGravity = false;

        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        //transform.position = new Vector3(-10, 0, -5);
        //transform.rotation = Quaternion.identity;

        playerCollider.enabled = true;

        //movementScript.enabled = true;

        //if (cameraClamp != null)
        //{
        //    cameraClamp.enabled = true;
        //}

        foreach (Renderer renderer in renderers){

            renderer.enabled = true;

        }

        

        handlingCollision = false;
    }

        /*private void FlingPlayer()
    {
        Vector3 flingDirection = Random.onUnitSphere;
        flingDirection.y = Mathf.Abs(flingDirection.y);

        float flingForce = 10f;
        rb.AddForce(flingDirection * flingForce, ForceMode.Impulse);
    
        Vector3 torqueAxis = Random.onUnitSphere;

        float torqueForce = 5f;
        rb.AddTorque(torqueAxis * torqueForce, ForceMode.Impulse);
    }*/

}
