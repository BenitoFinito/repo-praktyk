using System.Collections;
using UnityEngine;

public class Bf109DiveTrigger : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject parent;
    private PlayerCollisionHandler playerCollisionHandler;
    private bool stop = false;
    private float pozycja;

    void Start()
    {

        if (player != null)
        {
            playerCollisionHandler = player.GetComponent<PlayerCollisionHandler>();
        }
        else
        {
            Debug.LogError("Player GameObject not assigned.");
        }
    }

    void Update()
    {
        if (!stop && playerCollisionHandler != null && playerCollisionHandler.handlingCollision)
        {
            pozycja = player.transform.position.y;
            parent.transform.position = new Vector3(-10, pozycja, 10);
            animator.SetTrigger("Death");
            stop = true;
        }

        if(!playerCollisionHandler.handlingCollision){
            stop = false;
        }
    }
}
