using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private MainManager mainManager;

    public ParticleSystem exhaustEffect;
    public ParticleSystem gasUpEffect;
    public ParticleSystem collectEffect;
    public GameObject explosionEffect;

    public float horizontalSpeed = 8.8f;
    public float verticalSpeed = 4f;
    public float outOfGasSpeed = 2f;
    public float rotationSpeed = 100f;
    public float rotationResetSpeed = 100f;
    public float pullSpeed = 2f;

    private float forwardLimit = -2f;
    private float backwardLimit = -7.5f;

    private float carStartingZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();

        carStartingZ = rb.position.z;
    }

    void FixedUpdate()
    {
        if (!mainManager.gameOver)
        {
            HorizontalMovement();
            VerticalMovement();
            PullCarToStartingZ();
        }
        else
        {
            // have car slip back out of screen if runs out of gas
            DriftCarBackwards();
            exhaustEffect.gameObject.SetActive(false);
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

    private void VerticalMovement()
    {
        // move car forward and back on global axis
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 verticalMovement = verticalInput * Vector3.forward * verticalSpeed * Time.fixedDeltaTime;

        // stop vertical movement speed when at vertical limits
        if (rb.position.z > forwardLimit)
        {
            rb.position = new Vector3(rb.position.x, rb.position.y, forwardLimit);
        }
        else if (rb.position.z < backwardLimit)
        {
            rb.position = new Vector3(rb.position.x, rb.position.y, backwardLimit);
        }
        
        rb.MovePosition(rb.position + verticalMovement);
    }

    private void StraightenCar()
    {
        Quaternion currentRotation = rb.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationResetSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(newRotation);
    }

    private void PullCarToStartingZ()
    {
        // only pull to starting Z when a bit away from starting Z value to avoid jittering when not vert moving
        if (Math.Abs(rb.position.z - carStartingZ) > 0.08)
        {
            Vector3 pullDirection = (new Vector3(rb.position.x, rb.position.y, carStartingZ) - rb.position).normalized;
            Vector3 pullMovement = pullDirection * pullSpeed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + pullMovement);
        }
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
            Vector3 otherPosition = other.transform.position;
             Instantiate(collectEffect, otherPosition, Quaternion.identity);
            
            mainManager.FuelUp();
            SoundManager.Instance.PlaySound("gas");
            gasUpEffect.Play();
            Destroy(other);
        }
        else if (other.CompareTag("Bad") || other.CompareTag("Death"))
        {
            mainManager.GameOver();
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySound("explosion");
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
