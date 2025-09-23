using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private MainManager mainManager;

    public float speed = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
    }

    void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMovement = horizontalInput * transform.right * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + horizontalMovement);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Good"))
        {
            mainManager.FuelUp();
            Destroy(other);
        }
        else if (other.CompareTag("Bad") || other.CompareTag("Death"))
        {
            mainManager.GameOver();
            gameObject.SetActive(false);
        }
    }
}
