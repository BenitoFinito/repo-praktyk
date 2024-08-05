using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneratorScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] clouds;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    GameObject endPoint;

    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        startPos.z = 1;
        Prewarm();
        Invoke("AttemptSpawn", spawnInterval);
    }

    void SpawnCloud(Vector3 startPos)
    {
        int randomIndex = Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[randomIndex]);
        cloud.name = clouds[randomIndex].name;

        float startY = Random.Range(startPos.y - 3f, startPos.y + 3f);
        cloud.transform.position = new Vector3(startPos.x, startY ,startPos.z);


        float scale = Random.Range(8f, 12f);
        cloud.transform.localScale = new Vector2(scale, scale);

        float speed = Random.Range(0.5f, 1.5f);
        cloud.GetComponent<CloudScript>().StartMoving(speed, endPoint.transform.position.x);


    }

    void AttemptSpawn()
    {
        SpawnCloud(startPos);

        Invoke("AttemptSpawn", spawnInterval);
    }

    void Prewarm()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnPos = startPos + Vector3.left * (i * 4);
            SpawnCloud(spawnPos);
            
        }
    }
}
