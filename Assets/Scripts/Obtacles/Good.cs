using UnityEngine;

public class Good : Obstacle
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        pointValue = 5f;
        speed = 500f;
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
