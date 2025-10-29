using UnityEngine;

public class Bad : Obstacle
{
    public float badSpeed = 250f;
    public float badRotationSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = badSpeed;
        rotationSpeed = badRotationSpeed;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: play death effect at other.position
            base.OnTriggerEnter(other);
            Destroy(other);
        }
        else if (other.CompareTag("Backstop"))
        {
            Destroy(gameObject);
        }
    }
}
