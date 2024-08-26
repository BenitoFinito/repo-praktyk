using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinGeneratorScript : MonoBehaviour
{
    [SerializeField]
    //GameObject[] coins;
    GameObject coinObject;

    [SerializeField]
    float spawnInterval = 1f;

    [SerializeField]
    GameObject endPoint;

    [SerializeField]
    float speed = 1.5f;

    [System.Serializable]
    public struct coinPath
    {
        public float fromY;
        public float toY;
        public int step;

        public coinPath(float fromY, float toY, int step)
        {
            this.fromY = fromY;
            this.toY = toY;
            this.step = step;
        }
    }

    public List<coinPath> pathValues = new List<coinPath> 
    {
        new coinPath(0,2,2),
        new coinPath(0,-2,2),
        new coinPath(0,5,5),
        new coinPath(0,-5,5),
        new coinPath(0,30,5),
        new coinPath(30,-30,5),
        new coinPath(-30,30,5),
        new coinPath(30,-30,5),
        new coinPath(-30,0,5),
        new coinPath(5,-5,5),
    };

    //Only for read the results of coinPath list
    [SerializeField]
    private List<float> yValues = new List<float> {};

    Vector3 startPos;
    int currentIndex = 0;

    void Start()
    {
        startPos = transform.position;
        startPos.z = -4;
        Invoke("AttemptSpawn", spawnInterval);
    }

    void SpawnCoin(Vector3 startPos, float posY)
    {
        // if there was more coin object, we could use it
        //int randomIndex = Random.Range(0, coins.Length);
        //GameObject coin = Instantiate(coins[randomIndex]);
        GameObject coin = Instantiate(coinObject);
        coin.name = coinObject.name;

        float startY = startPos.y + posY;
        coin.transform.position = new Vector3(startPos.x, startY, startPos.z);

        coin.transform.localScale = new Vector2(5f, 5f);

        coin.GetComponent<CoinScript>().StartMoving(speed, endPoint.transform.position.x);
    }

    void AttemptSpawn()
    {
        foreach (var val in pathValues)
        {
            int direction = val.fromY < val.toY ? 1 : -1;
            for (float i = val.fromY; direction > 0 ? i <= val.toY : i >= val.toY ; i += val.step * direction)
            {
                yValues.Add(i);

            }
        }
        float posY = yValues[currentIndex];
        SpawnCoin(startPos, posY);

        currentIndex = (currentIndex + 1) % yValues.Count;

        Invoke("AttemptSpawn", spawnInterval);
    }

    /*
    float ScaleYValue(float value)
    {
        // rescale from range [-60, 60] to [-18, 18]
        return (value / 60) * 18;
    }

    // if needed (e.g. read data) uncomment and call the function
    /*
    float RescaleYValueBack(float value)
    {
        // rescale from range [-18, 18] to [-60, 60]
        return (value / 18) * 60;
    }
    */
}
