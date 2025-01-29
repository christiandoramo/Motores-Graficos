using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public float rotationSpeed = 5.0f; // Velocidade de rotação
    [SerializeField] private GameObject meteorsObject;
    [SerializeField] private Camera cam;
    [SerializeField] private float cameraDistance = 1000f;
    private ParticleSystem meteorsParticleSystem;
    bool newMeteorEmission = true;

    void Start()
    {
        if (meteorsObject == null) meteorsObject = transform.GetChild(0).gameObject;
        if (cam == null) cam = Camera.main;
        if (meteorsParticleSystem == null) meteorsParticleSystem = meteorsObject.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);

        if (newMeteorEmission == true) StartCoroutine(ParticleInterval());
    }

    private IEnumerator ParticleInterval() // pegando rotação da camera a cada emissao de meteoro
    {
        //meteorsObject.transform.position = cam.transform.position + cam.transform.forward * cameraDistance;
        //// transform.forward frente do vetor Z em relação a camera

        ////meteorsSystem.transform.rotation = cam.transform.rotation;
        //meteorsObject.transform.rotation = Quaternion.LookRotation(meteorsObject.transform.position - cam.transform.position);
        //// vira hemisferio emissor de meteoros para a camêra
        ///
        newMeteorEmission = false;
        meteorsObject.transform.SetPositionAndRotation(cam.transform.position + cam.transform.forward * cameraDistance, Quaternion.LookRotation(meteorsObject.transform.position - cam.transform.position));
        // vira hemisferio emissor de meteoros para a camêra
        //meteorsParticleSystem.main.startLifetime.constant
        yield return new WaitForSeconds(meteorsParticleSystem.main.startLifetime.constant);
        newMeteorEmission = true;
    }
}
