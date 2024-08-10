using System.Collections;
using UnityEngine;

public class Bf109DiveTrigger : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;
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

    }

    void Update()
    {
        if (!stop && playerCollisionHandler != null && playerCollisionHandler.handlingCollision)
        {
            source.PlayOneShot(clip1);
            source.PlayOneShot(clip2);

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
