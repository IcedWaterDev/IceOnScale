using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float xPositionMin;
    [SerializeField]
    private float xPositionMax;
    [SerializeField]
    private float dropHeight;
    private List<GameObject> obstacleList;
    [SerializeField]
    private List<GameObject> obstacleTemplates;
    [SerializeField]
    private float spawnInterval;
    private float timer;

    [SerializeField]
    private float spawnIntervalDecrement;
    [SerializeField]
    private float minInterval;
    private float intervalDecrementTimer;

    // Start is called before the first frame update
    void Start()
    {
        obstacleList = new List<GameObject>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        intervalDecrementTimer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            GenerateObstacle();
            timer -= spawnInterval;
        }

        DecreaseSpawnInterval();
    }

    public void GenerateObstacle()
    {
        int randomIndex = Random.Range(0, obstacleTemplates.Count);
        float randomPosX = Random.Range(xPositionMin, xPositionMax);

        GameObject obstacle = Instantiate(obstacleTemplates[randomIndex], new Vector3(randomPosX, dropHeight, obstacleTemplates[randomIndex].transform.position.z), Quaternion.identity);
        
        obstacleList.Add(obstacle);
    }

    private void DecreaseSpawnInterval()
    {
        if(intervalDecrementTimer > spawnIntervalDecrement)
        {
            intervalDecrementTimer -= spawnIntervalDecrement;

            if (spawnInterval > minInterval)
            {
                spawnInterval -= 0.1f;
            }
        }
    }
}
