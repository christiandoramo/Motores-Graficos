using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using System.Threading;

public class SpaceshipController : MonoBehaviour
{
    //public float acceleration = 10f;
    //public float maxSpeed = 20f;
    public float boostedSpeed = 60f;
    //public float rotationSpeed = 2f;
    public float deceleration = 10f;
    private Vector3 velocity;
    public float verticalSpeed = 5f;
    public float currentSpeed = 0f;

    public Light flashlight;

    private float currentMaxSpeed;

    public float bankAngle = 45f;
    public float bankSmoothness = 2f;
    // private float targetBankAngle = 0f;
    // private float currentBankAngle = 0f;

    //private TrailRenderer trailRenderer;
    //public TextMeshProUGUI speedText;

    private bool isBoosting = false;
    private bool flashlightOn;
    public Camera cam;

    [SerializeField] float surfaceDetectionRadius = 5f;
    [SerializeField] LayerMask surfaceMask;

    [Tooltip("Vetor usado pelo professor para pegar a frente da rotação nave")]
    private Vector3 targetForward = Vector3.zero;



    // novo movimento com física
    public Rigidbody rb;
    public float acceleration = 8f;
    public float speed = 20;
    public float maxSpeed = 40;

    public float rotationSpeed = 2;



    void Start()
    {
        targetForward = transform.forward;

        rb = rb == null ? transform.GetChild(0).GetComponent<Rigidbody>() : rb;
        currentMaxSpeed = maxSpeed; // coloca a velocidade atualmaaxima como velocidade maxima (para controlar vel boost de velocidade)
        cam = Camera.main;

        //rb.linearDamping = drag;
        //rb.angularDamping = drag;
    }

    void Update()
    {
        HandleMovement();
        HandleLight();
    }
    void HandleToggleLight()
    {
        if (Input.GetButtonDown("Fire2") && !flashlightOn)
        {
            flashlightOn = true; // Alterna o estado da lanterna.
            flashlight.enabled = true;
            flashlight.intensity = 2f;
            flashlight.spotAngle = 10f;
        }
        else if (Input.GetButtonDown("Fire2") && flashlightOn)
        {
            flashlightOn = false;
            flashlight.enabled = false;
        }
    }

    void HandleLight()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
        { //  toggle - ativa/desativa o rastro e boost
            isBoosting = !isBoosting;

        }
        if (!flashlightOn) return; // Só modifica se a lanterna estiver ligada.
        // Ajustar intensidade e ângulo com scroll do mouse.
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) != 0f)
        {
            flashlight.intensity = Mathf.Clamp(flashlight.intensity + scroll * 2f, 1f, 10f);
            flashlight.spotAngle = Mathf.Clamp(flashlight.spotAngle + scroll * 10f, 10f, 50f);
        }
    }

    void HandleMovement()
    {
        // Controle de movimentação pelo teclado
        float moveX = Input.GetAxis("Horizontal"); // Movimento lateral
        float moveZ = Input.GetAxis("Vertical");   // Movimento frente/trás
        float moveY = Input.GetAxis("Jump");       // Movimento vertical (subir/descer)

        // Calcula direção baseada na entrada
        Vector3 direction = transform.forward * moveZ + transform.right * moveX + transform.up * moveY;

        // Adiciona força na direção calculada
        rb.AddForce(direction * acceleration, ForceMode.Force);

        // Adiciona torque baseado na direção (apenas para demonstração)
        Vector3 torque = new Vector3(moveY, moveX, -moveZ); // Altere isso para seu caso específico
        rb.AddTorque(torque * acceleration/2, ForceMode.Force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, surfaceDetectionRadius);
    }

}
