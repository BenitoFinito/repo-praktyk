using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    private float speed;
    private float endPosX;

    public void StartMoving(float speed, float endPosX)
    {
        this.speed = speed;
        this.endPosX = endPosX;
    }
    void Update()
    {
        transform.position += Vector3.left * (Time.deltaTime * speed);

        if (transform.position.x < endPosX)
        {
            Destroy(gameObject);
        }
    }
}
