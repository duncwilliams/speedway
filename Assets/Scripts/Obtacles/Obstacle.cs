using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected float speed { private get; set; }
    protected Rigidbody rb;

    private Vector3 flowDirection = Vector3.back;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Flow();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }

    public virtual void Flow() 
    {
        rb.AddForce(flowDirection * speed * Time.deltaTime);
    }
}
