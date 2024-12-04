using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public float rotationSpeed = 0f;       // vel base da rotação
    public float orbitSpeed = 0f;          // vel de órbita/translacao
    public int rotationDirection;          // (1 ou -1) horário ou anti-horário
    public Transform sun = null;

    public float variationSpeed = 1.0f;    // aceleração que muda ao longo do tempo para simular mundo real...
    public float xAmplitude = 1.5f;        // range de variação no X
    public float yAmplitude = 1.7f;        // range da variação no Y
    public float zAmplitude = 1.3f;        // range da variação no Z
    private float orbitAngle = 0f;        
    public float orbitDistance;        
    public float orbitEccentricity = 1.5f; // excentricidade da elipse- tentativa de elipse com variação ao longo da orbita
    public float verticalOffset = 0.5f;

    void Start()
    {
        if (sun == null) sun = GameObject.FindWithTag("Sun").transform;

        // direção de rotação como -1 ou 1
        rotationDirection = UnityEngine.Random.Range(0, 2) * 2 - 1;

        // distância inicial de cada plaeta pro Sol
        orbitDistance = Vector3.Distance(transform.position, sun.position);

        // Ajusta a velocidade de órbita e rotação baseada no tamanho do planeta, se não especificada
        if (orbitSpeed == 0f) orbitSpeed = transform.lossyScale.x * .1f;
        if (rotationSpeed == 0f) rotationSpeed = transform.lossyScale.x * 15f;
    }

    void Update()
    {
        RotateSelf();       
        OrbitAroundSun();    
    }

    void OrbitAroundSun()
    {
        // Incrementa o ângulo de órbita baseado na velocidade e direção da órbita
        orbitAngle += rotationDirection * orbitSpeed * Time.deltaTime;

        // Calcula a posição ao longo da órbita elíptica com inclinação no eixo Y
        float xPosition = Mathf.Cos(orbitAngle) * orbitDistance;
        float yPosition = Mathf.Sin(orbitAngle * 0.5f) * orbitDistance * verticalOffset; 
        float zPosition = Mathf.Sin(orbitAngle) * orbitDistance * orbitEccentricity;

        // Define a posição do planeta na órbita elíptica inclinada ao redor do Sol
        transform.position = sun.position + new Vector3(xPosition, yPosition, zPosition);
    }

    void RotateSelf()
    {
        // aplica rotação própria com variação
        float yVariacao = Mathf.Sin(Time.time * variationSpeed + Mathf.PI / 2) * yAmplitude;
        float zVariacao = Mathf.Sin(Time.time * variationSpeed + Mathf.PI) * zAmplitude;

        // rotaciona de fato
        transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime,
                         rotationSpeed * Time.deltaTime * yVariacao,
                         rotationSpeed * Time.deltaTime * zVariacao, Space.Self); // em torno de si mesmo
    }
}
