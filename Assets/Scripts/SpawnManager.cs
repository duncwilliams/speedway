using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    
    private float spawnDelay = 1f;
    private Vector3 spawnPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = GameObject.Find("Spawn Point").transform.position;
        
        InvokeRepeating("SpawnObstacles", spawnDelay, spawnDelay);
    }

    public void SpeedUp(float level)
    {
        CancelInvoke("SpawnObstacles");
        spawnDelay /= level;
        InvokeRepeating("SpawnObstacles", spawnDelay, spawnDelay);
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnObstacles");
    }

    private void SpawnObstacles()
    {
        // Set random spawn location and random object index
        Vector3 spawnLocation = new Vector3(Random.Range(-3.5f, 3.5f), spawnPoint.y, spawnPoint.z);
        int index = Random.Range(0, obstacles.Length);

        Instantiate(obstacles[index], spawnLocation, obstacles[index].transform.rotation);
    }
}
