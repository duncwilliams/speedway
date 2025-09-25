using UnityEngine;

public class Good : Obstacle
{
    public float goodSpeed = 500f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = goodSpeed;
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
