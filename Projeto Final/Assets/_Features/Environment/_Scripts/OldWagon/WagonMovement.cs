using UnityEngine;

public class WagonMovement : MonoBehaviour
{
    public float forceMultiplier = 10f; // Multiplicador da for�a do motor
    public float brakeForce = 100f; // For�a do freio
    public GameObject frontalLeftWheel; // Frente esquerda
    public GameObject frontalRightWheel; // Frente direita
    public GameObject backLeftWheel; // Tr�s esquerda
    public GameObject backRightWheel; // Tr�s direita
    private WheelCollider frontalLeftWheelCollider; // Frente esquerda
    private WheelCollider frontalRightWheelCollider;  // Frente direita
    private WheelCollider backLeftWheelCollider; // Tr�s esquerda
    private WheelCollider backRightWheelCollider; //  Tr�s direita

    private bool isPushed = false; // Indica se est� sendo empurrado
    private float currentMotorForce = 0f; // For�a atual do motor
    private Rigidbody rb;

    void Start()
    {
        frontalLeftWheelCollider = frontalLeftWheel.GetComponent<WheelCollider>(); // Frente esquerda
        frontalRightWheelCollider = frontalRightWheel.GetComponent<WheelCollider>(); // Frente direita
        backLeftWheelCollider = backLeftWheel.GetComponent<WheelCollider>(); // Tr�s esquerda
        backRightWheelCollider = backRightWheel.GetComponent<WheelCollider>(); // Tr�s direita

        rb = GetComponent<Rigidbody>();
    }

    //void Update()
    //{
    //    if (isPushed)
    //    {
    //        backLeftWheelCollider.motorTorque = currentMotorForce;
    //        backRightWheelCollider.motorTorque = currentMotorForce;

    //        // Garante que as rodas dianteiras est�o alinhadas (sem dire��o)
    //        frontalLeftWheelCollider.steerAngle = 0;
    //        frontalRightWheelCollider.steerAngle = 0;

    //        UpdateWheels();
    //    }
    //    else if (rb.linearVelocity.magnitude != 0)
    //    {
    //        UpdateWheels();
    //        ApplyBrake();
    //    }
    //}
    void Update()
    {
        if (isPushed)
        {
            backLeftWheelCollider.brakeTorque = 0;
            backRightWheelCollider.brakeTorque = 0;
            frontalLeftWheelCollider.brakeTorque = 0;
            frontalRightWheelCollider.brakeTorque = 0;

            frontalLeftWheelCollider.motorTorque = currentMotorForce;
            frontalRightWheelCollider.motorTorque = currentMotorForce;
            backLeftWheelCollider.motorTorque = currentMotorForce;
            backRightWheelCollider.motorTorque = currentMotorForce;

            // Impede que as rodas dianteiras virem
            //frontalLeftWheelCollider.steerAngle = 0;
            //frontalRightWheelCollider.steerAngle = 0;
            //backLeftWheelCollider.steerAngle = 0;
            //backRightWheelCollider.steerAngle = 0;

            UpdateWheels();
        }
        else if (rb.linearVelocity.magnitude != 0)
        {
            UpdateWheels();
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 relativeVelocity = collision.relativeVelocity;

            float pushForce = relativeVelocity.z;
            currentMotorForce = pushForce * forceMultiplier;

            isPushed = true;
        }
    }

    private void OnCollisionExit(Collision collision) // futuramente corrigir usando um raycast para checar colis�o
    {
        // freio quando nao se mexe
        if (collision.collider.CompareTag("Player"))
        {
            isPushed = false;
            ApplyBrake();
        }
    }

    void UpdateWheels()
    {
        // movimento da roda 4x4
        float rotationAngleFL = frontalLeftWheelCollider.rpm * 360 * Time.deltaTime / 60f;
        frontalLeftWheel.transform.Rotate(0, 0, -rotationAngleFL, Space.Self); // Use Space.Self para rota��o local

        float rotationAngleFR = frontalRightWheelCollider.rpm * 360 * Time.deltaTime / 60f;
        frontalRightWheel.transform.Rotate(0, 0, rotationAngleFR, Space.Self);

        float rotationAngleBL = backLeftWheelCollider.rpm * 360 * Time.fixedDeltaTime / 60f;
        backLeftWheel.transform.Rotate(0, 0, -rotationAngleBL, Space.Self);

        float rotationAngleBR = backRightWheelCollider.rpm * 360 * Time.fixedDeltaTime / 60f;
        backRightWheel.transform.Rotate(0, 0, rotationAngleBR, Space.Self);
    }
}
