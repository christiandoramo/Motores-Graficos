using System.Collections;
using System.Linq;
using UnityEngine;

public class WagonMove : MonoBehaviour
{
    [SerializeField] float checkFrequency;
    [SerializeField] float reduceWheelRotation;
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
        CheckIfPlayerIsAbove();
        UpdateWheels();

    }
    void CheckIfPlayerIsAbove()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize * .5f, Quaternion.identity, playerMask);
        Collider collider = colliders.FirstOrDefault((collider) => collider != null);

        if (collider != null)
        {
            isBeingPushed = false;
            ApplyBrake();
        }
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
        rb.linearVelocity = Vector3.zero;
        // rb.linearDamping = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerMove>().isPushing = true;
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
            collision.transform.GetComponent<PlayerMove>().isPushing = false;
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
        localEulerFL.y = Mathf.Clamp(/*-90 + */steerAngleFL, -maxSteerAngle, maxSteerAngle); // Limita ângulos locais
        frontalLeftWheel.transform.localEulerAngles = localEulerFL;

        Vector3 localEulerFR = frontalRightWheel.transform.localEulerAngles;
        localEulerFR.y = Mathf.Clamp(/*90 + */steerAngleFR, -maxSteerAngle, maxSteerAngle); // Limita ângulos locais
        frontalRightWheel.transform.localEulerAngles = localEulerFR;

        // Atualiza a rotação das rodas traseiras
        Vector3 localEulerBL = backLeftWheel.transform.localEulerAngles;
        localEulerBL.y = Mathf.Clamp(/*-90 + */steerAngleBL, -maxSteerAngle, maxSteerAngle); // Limita ângulos locais
        backLeftWheel.transform.localEulerAngles = localEulerBL;

        Vector3 localEulerBR = backRightWheel.transform.localEulerAngles;
        localEulerBR.y = Mathf.Clamp(/*90 + */steerAngleBR, -maxSteerAngle, maxSteerAngle); // Limita ângulos locais
        backRightWheel.transform.localEulerAngles = localEulerBR;

        /// GIRO DO EIXO - giro do contato com o solo
        float rotationAngleFL = -frontalLeftWheelCollider.rpm * Time.deltaTime / reduceWheelRotation;
        frontalLeftWheel.transform.Rotate(0, rotationAngleFL, 0, Space.Self);

        float rotationAngleFR = -frontalRightWheelCollider.rpm * Time.deltaTime / reduceWheelRotation;
        frontalRightWheel.transform.Rotate(0, rotationAngleFR, 0, Space.Self);

        float rotationAngleBL = -backLeftWheelCollider.rpm * Time.deltaTime / reduceWheelRotation;
        backLeftWheel.transform.Rotate(0, rotationAngleBL, 0, Space.Self);

        float rotationAngleBR = -backRightWheelCollider.rpm * Time.deltaTime / reduceWheelRotation;
        backRightWheel.transform.Rotate(0, rotationAngleBR, 0, Space.Self);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
    private IEnumerator HandlePushForce(Collision collision)
    {

        while (isBeingPushed)
        {
            Vector3 relativeVelocity = collision.relativeVelocity;
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

            yield return new WaitForSeconds(checkFrequency); // Atualiza a cada segundo
        }

        yield return null;
    }

}
