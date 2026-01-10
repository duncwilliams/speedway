using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;

    private float spawnFreqIncrement = 0.10f;
    private float speedIncrement = 100f;
    private float spawnDelay = 1f;
    private Vector3 spawnPoint;

    public Bad bad;
    public Good good;

    public float startingGoodSpeed;
    public float startingBadSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = GameObject.Find("Spawn Point").transform.position;

        bad.badSpeed = startingBadSpeed;
        good.goodSpeed = startingGoodSpeed;

        InvokeRepeating("SpawnObstacles", spawnDelay, spawnDelay);
    }

    public void SpeedUp()
    {
        CancelInvoke("SpawnObstacles");
        spawnDelay -= spawnFreqIncrement;
        bad.badSpeed += speedIncrement;
        good.goodSpeed += speedIncrement;
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

        GameObject obstacle = obstacles[index];
        float randomYRotation = Random.Range(0f, 360f);

        Instantiate(obstacle, spawnLocation, Quaternion.Euler(obstacle.transform.rotation.x, randomYRotation, obstacle.transform.rotation.z));
    }
}
