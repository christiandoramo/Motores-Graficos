using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Motor Config")]
    [SerializeField] float reduceWheelRotation;
    [SerializeField] private float motorTorqueLimit = 1000f; // força do motor
    [SerializeField] private float motorTorque; // força do motor
    [SerializeField] private float steerTorque; // força da direção
    [SerializeField] private float brakeTorque; // força do freio

    [SerializeField] private float currentMotorTorque;
    [SerializeField] private float decelerationRate = 2500f;
    [SerializeField] private float accelerationRate = 1000f;
    [Header("References")]
    [SerializeField] private InputsService inputs;
    [SerializeField] private GameObject frontalLeftWheel; // Frente esquerda
    [SerializeField] private GameObject frontalRightWheel; // Frente direita
    [SerializeField] private GameObject backLeftWheel; // Trás esquerda
    [SerializeField] private GameObject backRightWheel; // Trás direita
    [SerializeField] private WheelCollider frontalLeftWheelCollider; // frente esquerda
    [SerializeField] private WheelCollider frontalRightWheelCollider; // frente direita
    [SerializeField] private WheelCollider backLeftWheelCollider; // tras esquerda
    [SerializeField] private WheelCollider backRightWheelCollider; // tras direita

    [Header("Camera config")][SerializeField] float camLookSpeed;
    [SerializeField] private float upDownRange = 80f;
    [SerializeField] private Transform camHolderTransform;
    float horizontalRotation = 0;
    float verticalRotation = 0;



    void Start()
    {
        currentMotorTorque = 0;
        // camHolderTransform = Camera.main.transform.parent; Colocar camHolderManualmente
    }

    void Update()
    {
        if (!GameManager.instance.isDriving)
        {
            return;
        }

        verticalRotation -= inputs.mouseInputY * camLookSpeed * Time.deltaTime;
        horizontalRotation += inputs.mouseInputX * camLookSpeed * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        camHolderTransform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

        backLeftWheelCollider.brakeTorque = 0;
        backRightWheelCollider.brakeTorque = 0;
        frontalLeftWheelCollider.brakeTorque = 0;
        frontalRightWheelCollider.brakeTorque = 0;

        float currentSteerTorque = inputs.xMove * steerTorque;
        frontalLeftWheelCollider.steerAngle = currentSteerTorque;
        frontalRightWheelCollider.steerAngle = currentSteerTorque;
        backLeftWheelCollider.steerAngle = currentSteerTorque;
        backRightWheelCollider.steerAngle = currentSteerTorque;


        if (inputs.brakeInput > 0)
        {


            if (currentMotorTorque > 0)
            {
                currentMotorTorque = Mathf.Min(0, currentMotorTorque - currentMotorTorque * decelerationRate * Time.deltaTime);
            }
            else if (currentMotorTorque < 0)
            {
                currentMotorTorque = Mathf.Max(0, currentMotorTorque + currentMotorTorque * decelerationRate * Time.deltaTime);
            }
            backLeftWheelCollider.brakeTorque = brakeTorque;
            backRightWheelCollider.brakeTorque = brakeTorque;
            frontalLeftWheelCollider.brakeTorque = brakeTorque;
            frontalRightWheelCollider.brakeTorque = brakeTorque;
        }
        else if (inputs.accelerateInput > 0 && inputs.zMove != 0)
        {
            currentMotorTorque = Mathf.Clamp(currentMotorTorque + -inputs.zMove * motorTorque * accelerationRate * Time.deltaTime, -motorTorqueLimit, motorTorqueLimit);
        }
        else if (inputs.zMove != 0)
        {
            currentMotorTorque = -inputs.zMove * motorTorque;
        }

        frontalLeftWheelCollider.motorTorque = currentMotorTorque;
        frontalRightWheelCollider.motorTorque = currentMotorTorque;
        backLeftWheelCollider.motorTorque = currentMotorTorque;
        backRightWheelCollider.motorTorque = currentMotorTorque;
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