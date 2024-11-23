using UnityEngine;
using TMPro;

public class SpaceshipController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    public float boostedSpeed = 60f;
    public float rotationSpeed = 2f;
    public float deceleration = 10f;
    private Vector3 velocity;
    public float verticalSpeed = 5f;
    private float currentSpeed = 0f;
    private float currentMaxSpeed;

    public float bankAngle = 45f;
    public float bankSmoothness = 2f;
    private float targetBankAngle = 0f;
    private float currentBankAngle = 0f;

    private TrailRenderer trailRenderer;
    public TextMeshProUGUI speedText;
    private CharacterController controller;

    private bool isBoosting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentMaxSpeed = maxSpeed; // coloca a velocidade atualmaaxima como velocidade maxima (para controlar vel boost de velocidade)
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }

    void Update()
    {
        HandleBoostToggle();
        HandleSpeed(); // velocidade de acordo com o estado atual
        HandleRotation();
        HandleMovement();   
        UpdateSpeedText(); 

        HandleBanking();     // suaviza a rotação lateral
    }

    void HandleBoostToggle()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
        { //  toggle - ativa/desativa o rastro e boost
            isBoosting = !isBoosting;
            trailRenderer.enabled = isBoosting; 
        }
    }

    void HandleSpeed() // controla a velocidade baseado no boosting
    {
        currentMaxSpeed = isBoosting ? boostedSpeed : maxSpeed;
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;
        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector3.right, mouseY);
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float moveY = 0f; // coloca velocidade vertical para descer ou subir

        if (Input.GetKey(KeyCode.E)) // cima
            moveY = verticalSpeed;
        else if (Input.GetKey(KeyCode.Q)) //baixo
            moveY = -verticalSpeed;

        Vector3 direction = transform.forward * moveZ + transform.right * moveX + transform.up * moveY;
        direction.Normalize();

        if (direction.magnitude > 0)
            currentSpeed += acceleration * Time.deltaTime;
        else
            currentSpeed -= deceleration * 3f * Time.deltaTime;

        currentSpeed = Mathf.Clamp(currentSpeed, 0, currentMaxSpeed);
        velocity = direction * currentSpeed;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleBanking()
    {
        float moveX = Input.GetAxis("Horizontal");
        targetBankAngle = -moveX * bankAngle;
        currentBankAngle = Mathf.Lerp(currentBankAngle, targetBankAngle, Time.deltaTime * bankSmoothness);
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentBankAngle);
    }

    void UpdateSpeedText()
    {
        speedText.text = $"Speed: {currentSpeed:F1} m/s";
    }
}
