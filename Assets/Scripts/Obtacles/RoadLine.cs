using UnityEngine;

public class RoadLine : Obstacle
{
    public float roadLineSpeed = 500f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = roadLineSpeed;
    }
}
