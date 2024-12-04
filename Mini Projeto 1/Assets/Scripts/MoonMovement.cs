using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMovement : MonoBehaviour
{
    public float rotationSpeed = 0f;       
    public float orbitSpeed = 0f;         

    public float xAmplitude = 1.5f;        // Amplitude da variação no eixo X
    public float yAmplitude = 1.7f;        // Amplitude da variação no eixo Y
    public float zAmplitude = 1.3f;        // Amplitude da variação no eixo Z
    public float variationSpeed = 1.0f;   

    public int rotationDirection;         
    public Transform planet = null;         
    public float orbitDistance;            // Distância da órbita em relação ao Sol
    public float orbitEccentricity = 1.5f; // Eccentricidade da elipse (alongamento horizontal)
    public float verticalOffset = 0.5f;    // Offset vertical para a inclinação em 3D

    private float orbitAngle = 0f;         // Ângulo atual da órbita

    void Start()
    {
        if (planet == null) planet = transform.parent;

        rotationDirection = Random.Range(0, 2) * 2 - 1;

        orbitDistance = Vector3.Distance(transform.position, planet.position);

        // Ajusta a velocidade de órbita e rotação baseada no tamanho do planet
        if (orbitSpeed == 0f) orbitSpeed = transform.lossyScale.x * 1f * Random.Range(7, 14);
        if (rotationSpeed == 0f) rotationSpeed = transform.lossyScale.x * 15f;
    }

    void Update()
    {
        RotateSelf();       
        OrbitAroundPlanet();    
    }

    void OrbitAroundPlanet()
    {
        // Incrementa o ângulo de órbita baseado na velocidade e direção da órbita
        orbitAngle += rotationDirection * orbitSpeed * Time.deltaTime;

        // Calcula a posição ao longo da órbita elíptica com inclinação no eixo Y
        float xPosition = Mathf.Cos(orbitAngle) * orbitDistance;
        float yPosition = Mathf.Sin(orbitAngle * 0.5f) * orbitDistance * verticalOffset; // Inclinação 3D no eixo Y
        float zPosition = Mathf.Sin(orbitAngle) * orbitDistance * orbitEccentricity;

        //movimenta com ma certa angulação baseado nodistancia entre a lua e o planeta
        transform.position = planet.position + new Vector3(xPosition, yPosition, zPosition);
    }

    void RotateSelf()
    {
        // Aplica rotação própria com variação
        float yVariacao = Mathf.Sin(Time.time * variationSpeed + Mathf.PI / 2) * yAmplitude;
        float zVariacao = Mathf.Sin(Time.time * variationSpeed + Mathf.PI) * zAmplitude;

        transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime,
                         rotationSpeed * Time.deltaTime * yVariacao,
                         rotationSpeed * Time.deltaTime * zVariacao, Space.Self);
    }
}
