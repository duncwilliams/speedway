using UnityEngine;

public class Good : Obstacle
{
    public float goodSpeed = 500f;
    public float goodRotationSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = goodSpeed;
        rotationSpeed = goodRotationSpeed;
    }
    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: play effect at gameObject.position
            base.OnTriggerEnter(other);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Backstop"))
        {
            Destroy(gameObject);
        }
    }
}
