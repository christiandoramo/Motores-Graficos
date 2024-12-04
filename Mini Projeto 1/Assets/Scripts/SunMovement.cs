using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public float rotationSpeed = 30f;      
    public float movementSpeed = 0.01f;    
    public float variationSpeed = 1.0f;     

    public float xAmplitude = 1.5f;          // Amplitude da variação no eixo X
    public float yAmplitude = 1.7f;          // Amplitude da variação no eixo Y
    public float zAmplitude = 1.3f;          // Amplitude da variação no eixo Z

    private Vector3 randomMovementDirection;

    void Start()
    {
        randomMovementDirection = new Vector3(
            Random.Range(-1f, 1f) * 0.1f,
            Random.Range(-1f, 1f) * 0.1f,
            Random.Range(-1f, 1f) * 0.1f
        ).normalized;
    }

    void Update()
    {
        // Aplicar rotação contínua ao Sol
        RotateSelf();

        // Movimento aleatório do Sol
        MoveRandomly();
    }

    void RotateSelf()
    {
            float yVariation = Mathf.Sin(Time.time * variationSpeed + Mathf.PI / 2) * yAmplitude;
            float zVariation = Mathf.Sin(Time.time * variationSpeed + Mathf.PI) * zAmplitude;

            // A rotação do Sol continua de forma constante, com variação nos eixos Y e Z
            transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime * yVariation, rotationSpeed * Time.deltaTime * zVariation, Space.Self);
        
    }

    void MoveRandomly()
    {
        transform.position += randomMovementDirection * movementSpeed * Time.deltaTime;
    }
}
