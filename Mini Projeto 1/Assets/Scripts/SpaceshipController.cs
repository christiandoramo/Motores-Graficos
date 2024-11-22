using UnityEngine;
using TMPro; // Certifique-se de incluir o namespace do TextMeshPro

public class SpaceshipController : MonoBehaviour
{
    // Parâmetros de movimento
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    public float boostedSpeed = 60f;
    public float rotationSpeed = 2f;
    public float deceleration = 10f;
    public float bankAngle = 45f;
    public float bankSmoothness = 2f;
    public float verticalSpeed = 5f;
    private float currentSpeed = 0f;
    private float currentMaxSpeed;

    // Referência ao Trail Renderer
    private TrailRenderer trailRenderer;

    // Referência ao TextMeshProUGUI no Canvas
    public TextMeshProUGUI speedText;

    private Vector3 velocity;
    private float targetBankAngle = 0f;
    private float currentBankAngle = 0f;

    private CharacterController controller;

    // Estado do boost
    private bool isBoosting = false;

    void Start()
    {
        // Obtém o CharacterController no GameObject
        controller = GetComponent<CharacterController>();
        currentMaxSpeed = maxSpeed; // Inicializa a velocidade máxima como a padrão

        // Obtém o Trail Renderer da nave
        trailRenderer = GetComponent<TrailRenderer>();

        // Certifica-se de que o rastro começa desativado
        trailRenderer.enabled = false;
    }

    void Update()
    {
        HandleBoostToggle(); // Alterna o estado do boost ao pressionar Shift
        HandleSpeed();       // Ajusta a velocidade de acordo com o estado atual
        HandleRotation();    // Controla a rotação com o mouse
        HandleMovement();    // Controla o movimento com WASD, Q, E
        HandleBanking();     // Suaviza a rotação lateral
        UpdateSpeedText();   // Atualiza o texto no Canvas com a velocidade
    }

    void HandleBoostToggle()
    {
        // Alterna o estado do boost ao pressionar Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = !isBoosting;
            trailRenderer.enabled = isBoosting; // Ativa/desativa o rastro
        }
    }

    void HandleSpeed()
    {
        // Ajusta a velocidade máxima com base no estado do boost
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
        float moveY = 0f;

        if (Input.GetKey(KeyCode.E))
            moveY = verticalSpeed;
        else if (Input.GetKey(KeyCode.Q))
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
