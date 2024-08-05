using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithCoin : MonoBehaviour
{
    public PointsHUD pointHUD;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Coin")
        {
            pointHUD.Points += 1;
            Destroy(collision.gameObject);
        }
    }
}
