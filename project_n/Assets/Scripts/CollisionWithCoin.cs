using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithCoin : MonoBehaviour
{
    public PointsHUD pointHUD;
    public AudioSource source;
    public AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Coin")
        {
            source.PlayOneShot(clip);
            pointHUD.Points += 1;
            Destroy(collision.gameObject);
        }
    }
}
