using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private MainManager mainManager;

    public float horizontalSpeed = 8.8f;
    public float outOfGasSpeed = 2f;
    public float rotationSpeed = 100f;
    public float rotationResetSpeed = 100f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
    }

    void FixedUpdate()
    {
        if (!mainManager.gameOver)
        {
            HorizontalMovement();
        }
        else
        {
            // have car slip back out of screen if runs out of gas
            DriftCarBackwards();
        }

        StraightenCar();
    }

    private void HorizontalMovement()
    {
        // move car left and right on global axis
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMovement = horizontalInput * Vector3.right * horizontalSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + horizontalMovement);

        // rotate car along the x axis to imitate swerving/steering like car does
        float rotation = horizontalInput * (rotationSpeed * 5) * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void StraightenCar()
    {
        Quaternion currentRotation = rb.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationResetSpeed * Time.fixedDeltaTime);
        
        rb.MoveRotation(newRotation);
    }

    private void DriftCarBackwards()
    {
        Vector3 backwardMovement = Vector3.back * outOfGasSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + backwardMovement);
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
