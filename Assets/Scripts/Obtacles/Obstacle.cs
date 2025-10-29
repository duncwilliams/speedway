using UnityEngine;
using UnityEngine.UIElements;

public class Obstacle : MonoBehaviour
{
    protected float speed { private get; set; }
    protected float rotationSpeed { private get; set; }
    protected Rigidbody rb;

    private Vector3 flowDirection = Vector3.back;
    private Vector3 rotationAxis = Vector3.up;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Flow();
        Rotate();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }

    public virtual void Flow()
    {
        rb.AddForce(flowDirection * speed * Time.deltaTime);
    }
    
    public virtual void Rotate()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
