using System.Collections;
using System.Linq;
using UnityEngine;

public class WagonCompleteMovement : MonoBehaviour
{
    public float forceMultiplier = 10f; // Multiplicador da for�a do motor
    public float steerForceMultiplier = 10f;
    public float brakeForce = 100f; // For�a do freio
    public GameObject frontalLeftWheel; // Frente esquerda
    public GameObject frontalRightWheel; // Frente direita
    public GameObject backLeftWheel; // Tr�s esquerda
    public GameObject backRightWheel; // Tr�s direita
    private WheelCollider frontalLeftWheelCollider; // Frente esquerda
    private WheelCollider frontalRightWheelCollider;  // Frente direita
    private WheelCollider backLeftWheelCollider; // Tr�s esquerda
    private WheelCollider backRightWheelCollider; //  Tr�s direita

    private bool isBeingPushed = false; // Indica se est� sendo empurrado
    private float currentMotorForce = 0f; // For�a atual do motor
    private float currentSteerForce = 0f;
    private Rigidbody rb;

    [SerializeField] private Vector3 boxSize;
    [SerializeField]
    private LayerMask playerMask;
    private Coroutine coroutine = null;

    void Start()
    {
        frontalLeftWheelCollider = frontalLeftWheel.GetComponent<WheelCollider>(); // Frente esquerda
        frontalRightWheelCollider = frontalRightWheel.GetComponent<WheelCollider>(); // Frente direita
        backLeftWheelCollider = backLeftWheel.GetComponent<WheelCollider>(); // Tr�s esquerda
        backRightWheelCollider = backRightWheel.GetComponent<WheelCollider>(); // Tr�s direita

        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!isBeingPushed && rb.linearVelocity.magnitude != 0)
        {
            ApplyBrake();
        }

        UpdateWheels();

    }


    private void ApplyBrake()
    {
        frontalLeftWheelCollider.motorTorque = 0;
        frontalRightWheelCollider.motorTorque = 0;
        backLeftWheelCollider.motorTorque = 0;
        backRightWheelCollider.motorTorque = 0;

        float _brakeForce = Mathf.Pow(forceMultiplier, 2) * brakeForce;
        backLeftWheelCollider.brakeTorque = _brakeForce;
        backRightWheelCollider.brakeTorque = _brakeForce;
        frontalLeftWheelCollider.brakeTorque = _brakeForce;
        frontalRightWheelCollider.brakeTorque = _brakeForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().isPushing = true;
            isBeingPushed = true;
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(HandlePushForce(collision));
        }
    }

    private void OnCollisionExit(Collision collision) // futuramente corrigir usando um raycast para checar colis�o
    {
        // Quando o jogador parar de empurrar, aplica o freio
        if (collision.collider.CompareTag("Player"))
        {
            isBeingPushed = false;
            collision.transform.GetComponent<PlayerController>().isPushing = false;
            ApplyBrake();
        }
    }
    void UpdateWheels()
    {
        // ANGULAÇÃO DA RODA EM RELAÇÃO A CURVA

        float maxSteerAngle = 45f;

        // Limita os valores do steerAngle
        float steerAngleFL = Mathf.Clamp(frontalLeftWheelCollider.steerAngle, -maxSteerAngle, maxSteerAngle);
        float steerAngleFR = Mathf.Clamp(frontalRightWheelCollider.steerAngle, -maxSteerAngle, maxSteerAngle);
        float steerAngleBL = Mathf.Clamp(backLeftWheelCollider.steerAngle, -maxSteerAngle, maxSteerAngle);
        float steerAngleBR = Mathf.Clamp(backRightWheelCollider.steerAngle, -maxSteerAngle, maxSteerAngle);

        // Atualiza a rotação das rodas dianteiras
        Vector3 localEulerFL = frontalLeftWheel.transform.localEulerAngles;
        localEulerFL.y = Mathf.Clamp(-90 + steerAngleFL, -180f, 180f); // Limita ângulos locais
        frontalLeftWheel.transform.localEulerAngles = localEulerFL;

        Vector3 localEulerFR = frontalRightWheel.transform.localEulerAngles;
        localEulerFR.y = Mathf.Clamp(90 + steerAngleFR, -180f, 180f); // Limita ângulos locais
        frontalRightWheel.transform.localEulerAngles = localEulerFR;

        // Atualiza a rotação das rodas traseiras
        Vector3 localEulerBL = backLeftWheel.transform.localEulerAngles;
        localEulerBL.y = Mathf.Clamp(-90 + steerAngleBL, -180f, 180f); // Limita ângulos locais
        backLeftWheel.transform.localEulerAngles = localEulerBL;

        Vector3 localEulerBR = backRightWheel.transform.localEulerAngles;
        localEulerBR.y = Mathf.Clamp(90 + steerAngleBR, -180f, 180f); // Limita ângulos locais
        backRightWheel.transform.localEulerAngles = localEulerBR;

        /// GIRO NO EIXO LOCAL Z - Giro infinito da roda
        float rotationAngleFL = frontalLeftWheelCollider.rpm * Time.deltaTime / 60f * 6f;
        frontalLeftWheel.transform.Rotate(0, 0, -rotationAngleFL, Space.Self);

        float rotationAngleFR = frontalRightWheelCollider.rpm * Time.deltaTime / 60f * 6f;
        frontalRightWheel.transform.Rotate(0, 0, rotationAngleFR, Space.Self);

        float rotationAngleBL = backLeftWheelCollider.rpm * Time.deltaTime / 60f * 6f;
        backLeftWheel.transform.Rotate(0, 0, -rotationAngleBL, Space.Self);

        float rotationAngleBR = backRightWheelCollider.rpm * Time.deltaTime / 60f * 6f;
        backRightWheel.transform.Rotate(0, 0, rotationAngleBR, Space.Self);
    }

    private IEnumerator HandlePushForce(Collision collision)
    {
        Vector3 relativeVelocity = collision.relativeVelocity;
        while (isBeingPushed)
        {
            float pushForceZ = relativeVelocity.z;

            float pushForceX = relativeVelocity.x;

            currentMotorForce = pushForceZ * forceMultiplier;

            currentSteerForce = pushForceX * steerForceMultiplier;

            backLeftWheelCollider.brakeTorque = 0;
            backRightWheelCollider.brakeTorque = 0;
            frontalLeftWheelCollider.brakeTorque = 0;
            frontalRightWheelCollider.brakeTorque = 0;

            frontalLeftWheelCollider.steerAngle = currentSteerForce;
            frontalRightWheelCollider.steerAngle = currentSteerForce;
            backLeftWheelCollider.steerAngle = currentSteerForce;
            backRightWheelCollider.steerAngle = currentSteerForce;

            frontalLeftWheelCollider.motorTorque = currentMotorForce;
            frontalRightWheelCollider.motorTorque = currentMotorForce;
            backLeftWheelCollider.motorTorque = currentMotorForce;
            backRightWheelCollider.motorTorque = currentMotorForce;

            yield return new WaitForSeconds(1f); // atualiza a cada segundo
            relativeVelocity = -collision.relativeVelocity;
        }
        yield return null;

    }
}
