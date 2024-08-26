using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGeneratorScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] houses;

    [SerializeField]
    float spawnInterval = 1f;

    [SerializeField]
    GameObject endPoint;

    [SerializeField]
    float speed = 1.44f;

    public float rangePositive = 0f, rangeNegative = 2f;

    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        Prewarm();
        Invoke("AttemptSpawn", spawnInterval);
    }

    void SpawnHouse(Vector3 startPos)
    {
        int randomIndex = Random.Range(0, houses.Length);
        GameObject house = Instantiate(houses[randomIndex]);
        house.name = houses[randomIndex].name;

        float startY = Random.Range(startPos.y - rangeNegative, startPos.y + rangePositive);
        house.transform.position = new Vector3(startPos.x, startY, startPos.z);

        house.GetComponent<HouseScript>().StartMoving(speed, endPoint.transform.position.x);


    }

    void AttemptSpawn()
    {
        SpawnHouse(startPos);

        Invoke("AttemptSpawn", spawnInterval);
    }

    void Prewarm()
    {
        for (int i = 0; i < 9; i++)
        {
            Vector3 spawnPos = startPos + Vector3.left * (i * 5);
            SpawnHouse(spawnPos);

        }
    }
}
