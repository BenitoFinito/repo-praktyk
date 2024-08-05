using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGeneratorScript : MonoBehaviour
{

    [SerializeField]
    GameObject[] stars;

    [SerializeField]
    float spawnInterval = 1f;

    [SerializeField]
    GameObject endPoint;

    [SerializeField]
    float speed = 1f;
    float previousStartY = 0;

    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        Prewarm();
        Invoke("AttemptSpawn", spawnInterval);
    }

    void SpawnStar(Vector3 startPos)
    {
        int randomIndex = Random.Range(0, stars.Length);
        GameObject star = Instantiate(stars[randomIndex]);
        star.name = stars[randomIndex].name;

        float startY = Random.Range(startPos.y - 3.5f, startPos.y + 2f);
        if(previousStartY != 0)
        { 
            if(startY <  previousStartY + 1f && startY >= previousStartY) 
            {
                startY = startY + 1.5f;
            } else if(startY > previousStartY - 1f && startY < previousStartY)
            {
                startY = startY - 1.5f;
            }
        }
        previousStartY = startY;
        star.transform.position = new Vector3(startPos.x, startY, startPos.z);

        float scale = Random.Range(1.5f, 4.5f);
        star.transform.localScale = new Vector2(scale, scale);

        float rotate = Random.Range(-45f, 45f);
        star.transform.rotation = Quaternion.Euler(0,0,rotate);

        star.GetComponent<StarScript>().StartMoving(speed, endPoint.transform.position.x);



    }

    void AttemptSpawn()
    {
        SpawnStar(startPos);

        Invoke("AttemptSpawn", spawnInterval);
    }

    void Prewarm()
    {
        for (int i = 0; i < 12; i++)
        {
            Vector3 spawnPos = startPos + Vector3.left * (i * 3);
            SpawnStar(spawnPos);

        }
    }
}
