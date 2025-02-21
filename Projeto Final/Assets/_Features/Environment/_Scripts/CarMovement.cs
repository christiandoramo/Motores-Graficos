using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float reduceWheelRotation;
    public float motorTorque; // força do motor
    public float steerTorque; // força da direção
    public float brakeTorque; // força do freio
    public GameObject frontalLeftWheel; // Frente esquerda
    public GameObject frontalRightWheel; // Frente direita
    public GameObject backLeftWheel; // Trás esquerda
    public GameObject backRightWheel; // Trás direita
    public WheelCollider frontalLeftWheelCollider; // frente esquerda
    public WheelCollider frontalRightWheelCollider; // frente direita
    public WheelCollider backLeftWheelCollider; // tras esquerda
    public WheelCollider backRightWheelCollider; // tras direita

    private float currentMotorTorque;
    [SerializeField] private float decelerationRate = 10f; // Taxa de desaceleração

    [Header("References")]
    [SerializeField] private InputsService inputs;

    void Start()
    {
        currentMotorTorque = motorTorque;
    }

    void Update()
    {
        if (!GameManager.instance.isDriving)
        {
            return;
        }


        float pushForceZ = -inputs.zMove * currentMotorTorque;
        float pushForceX = inputs.xMove * steerTorque;

        float currentMotorForce = pushForceZ * currentMotorTorque;
        float currentSteerForce = pushForceX * steerTorque;

        backLeftWheelCollider.brakeTorque = 0;
        backRightWheelCollider.brakeTorque = 0;
        frontalLeftWheelCollider.brakeTorque = 0;
        frontalRightWheelCollider.brakeTorque = 0;

        frontalLeftWheelCollider.steerAngle = currentSteerForce;
        frontalRightWheelCollider.steerAngle = currentSteerForce;
        backLeftWheelCollider.steerAngle = currentSteerForce;
        backRightWheelCollider.steerAngle = currentSteerForce;

        if (inputs.accelerateInput > 0)
        {
            currentMotorTorque = Mathf.Min(motorTorque, currentMotorTorque + decelerationRate * Time.deltaTime);
        }
        else if (inputs.brakeInput >0)
        {
            currentMotorTorque = Mathf.Max(0, currentMotorTorque - decelerationRate * Time.deltaTime);

            float _brakeForce = Mathf.Pow(motorTorque, 2) * brakeTorque;
            backLeftWheelCollider.brakeTorque = _brakeForce;
            backRightWheelCollider.brakeTorque = _brakeForce;
            frontalLeftWheelCollider.brakeTorque = _brakeForce;
            frontalRightWheelCollider.brakeTorque = _brakeForce;
        }
        else
        {
            currentMotorTorque = motorTorque;
        }

        frontalLeftWheelCollider.motorTorque = currentMotorForce;
        frontalRightWheelCollider.motorTorque = currentMotorForce;
        backLeftWheelCollider.motorTorque = currentMotorForce;
        backRightWheelCollider.motorTorque = currentMotorForce;

        UpdateWheels();
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
}