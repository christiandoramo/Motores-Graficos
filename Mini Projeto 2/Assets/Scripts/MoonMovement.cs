using UnityEngine;

public class MoonMovement : MonoBehaviour
{
    public float rotationSpeed = 0f;       // Velocidade base da rotação
    public float orbitSpeed = 0f;          // Velocidade de órbita para o planeta

    public float xAmplitude = 1.5f;        // Amplitude da variação no eixo X
    public float yAmplitude = 1.7f;        // Amplitude da variação no eixo Y
    public float zAmplitude = 1.3f;        // Amplitude da variação no eixo Z
    public float variationSpeed = 1.0f;    // Velocidade com que a variação muda ao longo do tempo

    public int rotationDirection;          // Direção da rotação (1 ou -1)
    public Transform planet = null;           // Referência ao Sol
    public float orbitDistance;            // Distância da órbita em relação ao Sol
    public float orbitEccentricity = 1.5f; // Eccentricidade da elipse (alongamento horizontal)
    public float verticalOffset = 0.5f;    // Offset vertical para a inclinação em 3D

    private float orbitAngle = 0f;         // Ângulo atual da órbita

    void Start()
    {
        if (planet == null) planet = transform.parent;

        // Define a direção de rotação como -1 ou 1 aleatoriamente
        rotationDirection = Random.Range(0, 2) * 2 - 1;

        // Calcula a distância inicial do planeta ao Sol
        orbitDistance = Vector3.Distance(transform.position, planet.position);

        // Ajusta a velocidade de órbita e rotação baseada no tamanho do planeta, se não especificada
        if (orbitSpeed == 0f) orbitSpeed = transform.lossyScale.x * 1f * Random.Range(7, 14);
        if (rotationSpeed == 0f) rotationSpeed = transform.lossyScale.x * 15f;
    }

    void Update()
    {
        RotateSelf();        // Rotação própria do planeta
        OrbitAroundPlanet();    // Translação em órbita elíptica e inclinada em 3D
    }

    void OrbitAroundPlanet()
    {
        // Incrementa o ângulo de órbita baseado na velocidade e direção da órbita
        orbitAngle += rotationDirection * orbitSpeed * Time.deltaTime;

        // Calcula a posição ao longo da órbita elíptica com inclinação no eixo Y
        float xPosition = Mathf.Cos(orbitAngle) * orbitDistance;
        float yPosition = Mathf.Sin(orbitAngle * 0.5f) * orbitDistance * verticalOffset; // Inclinação 3D no eixo Y
        float zPosition = Mathf.Sin(orbitAngle) * orbitDistance * orbitEccentricity;

        // Define a posição do planeta na órbita elíptica inclinada ao redor do Sol
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
