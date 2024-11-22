using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public float rotationSpeed = 30f;        // Velocidade base da rotação
    public float movementSpeed = 0.01f;       // Velocidade de movimento aleatório do Sol
    public float variationSpeed = 1.0f;      // Velocidade com que a variação muda ao longo do tempo

    public float xAmplitude = 1.5f;          // Amplitude da variação no eixo X
    public float yAmplitude = 1.7f;          // Amplitude da variação no eixo Y
    public float zAmplitude = 1.3f;          // Amplitude da variação no eixo Z

    private Vector3 randomMovementDirection; // Direção aleatória de movimento

    void Start()
    {
        // Inicializando a direção de movimento aleatória com valores entre -1 e 1 para cada eixo
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
            // Aplica rotação contínua com variação em torno do eixo próprio do Sol
            float yVariation = Mathf.Sin(Time.time * variationSpeed + Mathf.PI / 2) * yAmplitude;
            float zVariation = Mathf.Sin(Time.time * variationSpeed + Mathf.PI) * zAmplitude;

            // A rotação do Sol continua de forma constante, com variação nos eixos Y e Z
            transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime * yVariation, rotationSpeed * Time.deltaTime * zVariation, Space.Self);
        
    }

    void MoveRandomly()
    {
        // Aqui, você pode controlar o limite do movimento, se necessário, ou mantê-lo sem restrições
        transform.position += randomMovementDirection * movementSpeed * Time.deltaTime;
    }
}
