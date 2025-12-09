using UnityEngine;

public class SpeedBump : Obstacle
{
    public float speedBumpSpeed = 500f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = speedBumpSpeed;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Backstop"))
        {
            Destroy(gameObject);
        }
    }
}
