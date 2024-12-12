using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using System.Linq;

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
    private float targetBankAngle = 0f;
    private float currentBankAngle = 0f;

    private TrailRenderer trailRenderer;
    //public TextMeshProUGUI speedText;
    private CharacterController controller;

    private bool isBoosting = false;
    private bool flashlightOn;
    public Camera cam;

    [SerializeField] float surfaceDetectionRadius = 5f;
    [SerializeField] LayerMask surfaceMask;

    [Tooltip("Vetor usado pelo professor para pegar a frente da rotação nave")]
    private Vector3 targetForward = Vector3.zero;
    public float acceleration = 25;
    public float speed = 100;
    public float maxSpeed = 100;
    public float sidewaysSpeed = 25;
    public float rotationSpeed = 2;
    // create a target direction vector



    void Start()
    {
        targetForward = transform.forward;

        //controller = GetComponent<CharacterController>();
        currentMaxSpeed = maxSpeed; // coloca a velocidade atualmaaxima como velocidade maxima (para controlar vel boost de velocidade)
        //trailRenderer = GetComponent<TrailRenderer>();
        //trailRenderer.enabled = false;
        cam = Camera.main;
    }

    void Update()
    {
        // if (!cam.CompareTag("MainCamera")) return;
        //HandleBoostToggle();
        //HandleSpeed(); // velocidade de acordo com o estado atual
        HandleMovement();
        //HandleRotation();
        //UpdateSpeedText();
        HandleToggleLight();
        HandleLightIntensity();
        //HandleBanking();     // suaviza a rotação lateral
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

    //void HandleLight()
    //{
    //    // Aumenta ou diminui a intensidade conforme o estado.
    //    if (Input.GetButton("Fire1") && flashlight.intensity < 5f)
    //    {
    //        flashlight.intensity = Mathf.Clamp(flashlight.intensity + 1.25f * Time.deltaTime, 0f, 5f);
    //    }
    //    else if (flashlight.intensity > 0)
    //    {
    //        flashlight.intensity = Mathf.Clamp(flashlight.intensity - 2.5f * Time.deltaTime, 0f, 5f);
    //    }
    //}
    void HandleLightIntensity()
    {
        if (!flashlightOn) return; // Só modifica se a lanterna estiver ligada.
        // Ajustar intensidade e ângulo com scroll do mouse.
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) != 0f)
        {
            flashlight.intensity = Mathf.Clamp(flashlight.intensity + scroll * 2f, 1f, 10f);
            flashlight.spotAngle = Mathf.Clamp(flashlight.spotAngle + scroll * 10f, 10f, 50f);
        }
    }

    void HandleBoostToggle()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
        { //  toggle - ativa/desativa o rastro e boost
            isBoosting = !isBoosting;
            //trailRenderer.enabled = isBoosting;
        }
    }

    void HandleSpeed() // controla a velocidade baseado no boosting
    {
        currentMaxSpeed = isBoosting ? boostedSpeed : maxSpeed;
    }

    void HandleRotation()
    {
        //float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;
        //transform.Rotate(Vector3.right, mouseY);
        //float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        //transform.Rotate(Vector3.up, mouseX);

        // Grab the mouse delta movement
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        // Rotate the target direction according to mouse coordinates
        targetForward = Quaternion.AngleAxis(deltaX * rotationSpeed, transform.up) * targetForward;
        targetForward = Quaternion.AngleAxis(-deltaY * rotationSpeed, transform.right) * targetForward;

        transform.forward = Vector3.Lerp(transform.forward, targetForward, Time.deltaTime * rotationSpeed);

        Collider[] colliders = Physics.OverlapSphere(transform.position, surfaceDetectionRadius, surfaceMask);
        Collider collider = colliders.FirstOrDefault((collider) => collider != null);
        if (collider != null) handleSurfaceDetection(collider);

    }

    void HandleMovement()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        // Rotate the target direction according to mouse coordinates
        targetForward = Quaternion.AngleAxis(deltaX * rotationSpeed, transform.up) * targetForward;
        targetForward = Quaternion.AngleAxis(-deltaY * rotationSpeed, transform.right) * targetForward;

        transform.forward = Vector3.Lerp(transform.forward, targetForward, Time.deltaTime * rotationSpeed);

        // Naturally deaccelerate the spaceship
        speed -= speed * 0.1f * Time.deltaTime;

        // Accelerate the spaceship if W is pressed
        if (Input.GetKey(KeyCode.W))
        {
            speed += acceleration * Time.deltaTime;
        }
        else
        {
            speed -= acceleration * Time.deltaTime;
        }

        // Clamp the speed between 0 and the maxSpeed
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        // Move the spaceship forward
        transform.position += transform.forward * speed * Time.deltaTime;

        // Make the A and D keys to move the spaceship left and right
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * sidewaysSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * sidewaysSpeed * Time.deltaTime;
        }

        // Make the Q and E keys to move the spaceship up and down
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position -= transform.up * sidewaysSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += transform.up * sidewaysSpeed * Time.deltaTime;
        }

        // Iterate on all prefabs of CelestialBody in the scene
        foreach (var celestialBody in GameObject.FindGameObjectsWithTag("CelestialBody"))
        {
            // Radius of the celestial body
            // get the "Body" component of the CelestialBody prefab
            GameObject body = celestialBody.transform.Find("Body").gameObject;
            float radius = body.transform.localScale.x / 2;

            // Calculate the distance between the spaceship and the celestial body surface
            float distanceToSurface = Vector3.Distance(transform.position, celestialBody.transform.position) - radius;

            // Gravitational influence of the celestial body
            float gInfluence = 1.0f * radius;

            float t = 1 - Mathf.Clamp01(distanceToSurface / gInfluence);

            // If the distance is lower than gravitational_influence times the radius of the celestial body
            // we consider the spaceship is entering its gravitational field
            if (distanceToSurface < gInfluence)
            {
                Vector3 targetUp = (transform.position - celestialBody.transform.position).normalized;
                Vector3 currentUp = Vector3.Lerp(transform.up, targetUp, t);
                transform.LookAt(transform.position + transform.forward, currentUp);
            }
        }
    }

    void handleSurfaceDetection(Collider collider)
    {
        ////////////////////////////////// PARTE DO SCRIPT DO PROFESSOR - VOO SOBRE SUPERFICIE ////////////////////////////////////////////
        // Radius of the celestial body
        // get the "Body" component of the CelestialBody prefab
        GameObject body = collider.gameObject;
        float radius = body.transform.localScale.x / 2;

        // Calculate the distance between the spaceship and the celestial body surface
        float distanceToSurface = Vector3.Distance(transform.position, body.transform.position) - radius;

        // Gravitational influence of the celestial body
        float gInfluence = 1.0f * radius;

        float t = 1 - Mathf.Clamp01(distanceToSurface / gInfluence);

        // If the distance is lower than gravitational_influence times the radius of the celestial body
        // we consider the spaceship is entering its gravitational field
        if (distanceToSurface < gInfluence)
        {
            Debug.Log("Colidiu");
            Vector3 targetUp = (transform.position - body.transform.position).normalized;
            Vector3 currentUp = Vector3.Lerp(transform.up, targetUp, t);
            transform.LookAt(transform.position + transform.forward, currentUp);
        }
    }

    void HandleBanking()
    {
        float moveX;
        //if (transform.CompareTag("Player"))
        //{
        moveX = Input.GetAxis("Horizontal");
        //}
        //else
        //{
        //    moveX = Input.GetAxis("ArrowHorizontal");
        //}
        targetBankAngle = -moveX * bankAngle;
        currentBankAngle = Mathf.Lerp(currentBankAngle, targetBankAngle, Time.deltaTime * bankSmoothness);
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentBankAngle);
    }

    void UpdateSpeedText()
    {
        //speedText.text = $"Speed: {velocity} m/s";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, surfaceDetectionRadius);
    }

    //void HandleMovement()
    //{
    //    float moveX;
    //    float moveZ;
    //    float moveY = 0f; // coloca velocidade vertical para descer ou subir
    //    moveX = Input.GetAxis("Horizontal");
    //    moveZ = Input.GetAxis("Vertical");

    //    if (Input.GetKey(KeyCode.E)) // cima
    //        moveY = verticalSpeed;
    //    else if (Input.GetKey(KeyCode.Q)) //baixo
    //        moveY = -verticalSpeed;

    //    Vector3 direction = transform.forward * moveZ * .5f + transform.right * moveX * .5f + transform.up * moveY;
    //    direction.Normalize();

    //    if (direction.magnitude > 0)
    //        currentSpeed += acceleration * Time.deltaTime;
    //    else
    //        currentSpeed -= deceleration * 3f * Time.deltaTime;

    //    currentSpeed = Mathf.Clamp(currentSpeed, 0, currentMaxSpeed);
    //    velocity = direction * currentSpeed;
    //    controller.Move(velocity * Time.deltaTime);
    //    // handleSurfaceDetection(); // handle face direction
    //}
}
