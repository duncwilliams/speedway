using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject roadLine;
    public GameObject speedBump;

    private float speedIncrement = 0.12f;
    private float spawnDelay = 1f;
    private Vector3 spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = GameObject.Find("Spawn Point").transform.position;

        InvokeRepeating("SpawnObstacles", spawnDelay, spawnDelay);
        InvokeRepeating("SpawnRoadLines", 0f, spawnDelay);
        InvokeRepeating("SpawnSpeedBump", 5f, 5f);
    }

    public void SpeedUp()
    {
        CancelInvoke("SpawnObstacles");
        CancelInvoke("SpawnRoadLines");
        CancelInvoke("SpawnSpeedBump");
        spawnDelay -= speedIncrement;
        InvokeRepeating("SpawnObstacles", spawnDelay, spawnDelay);
        InvokeRepeating("SpawnRoadLines", 0.2f, spawnDelay);
        InvokeRepeating("SpawnSpeedBump", 5f, 5f);
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

        GameObject obstacle = obstacles[index];
        float randomYRotation = Random.Range(0f, 360f);

        Instantiate(obstacle, spawnLocation, Quaternion.Euler(obstacle.transform.rotation.x, randomYRotation, obstacle.transform.rotation.z));
    }

    private void SpawnRoadLines()
    {
        Vector3 spawnLocation = new Vector3(spawnPoint.x, 0.05f, spawnPoint.z);
        Instantiate(roadLine, spawnLocation, roadLine.transform.rotation);
    }

    private void SpawnSpeedBump()
    {
        Vector3 spawnLocation = new Vector3(0f, 0f, spawnPoint.z);
        Instantiate(speedBump, spawnLocation, speedBump.transform.rotation);
    }
}
