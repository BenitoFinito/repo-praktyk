using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuchtballonGeneratorScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] luchtballons;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    GameObject endPoint;

    public float rangePositive = 18f, rangeNegative = 18f;

    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        startPos.z = 1;
        Prewarm();
        Invoke("AttemptSpawn", spawnInterval);
    }

    void SpawnLuchtballon(Vector3 startPos)
    {
        int randomIndex = Random.Range(0, luchtballons.Length);
        GameObject luchtballon = Instantiate(luchtballons[randomIndex]);
        luchtballon.name = luchtballons[randomIndex].name;

        float startY = Random.Range(startPos.y - rangeNegative, startPos.y + rangePositive);
        float startZ = Random.Range(0, 2) == 1 ? 1 : -7;
        luchtballon.transform.position = new Vector3(startPos.x, startY, startZ);


        float scale = Random.Range(8f, 15f);
        luchtballon.transform.localScale = new Vector2(scale, scale);

        float speed = Random.Range(0.5f, 1.5f);
        luchtballon.GetComponent<LuchtballonScript>().StartMoving(speed, endPoint.transform.position.x);


    }

    void AttemptSpawn()
    {
        SpawnLuchtballon(startPos);

        Invoke("AttemptSpawn", spawnInterval);
    }

    void Prewarm()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPos = startPos + Vector3.left * (i * 4);
            SpawnLuchtballon(spawnPos);

        }
    }
}
