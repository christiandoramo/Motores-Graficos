using UnityEngine;

public class AlignDirectionalLightToCamera : MonoBehaviour
{
    public Light directionalLight; // A luz direcional
    public Camera targetCamera;    // A câmera de referência

    void Update()
    {
        if (directionalLight != null && targetCamera != null)
        {
            // Calcula a direção da luz para a câmera
            Vector3 directionToCamera = targetCamera.transform.position - directionalLight.transform.position;

            // Inverte a direção para simular luz vinda "de trás da câmera"
            directionToCamera = -directionToCamera.normalized;

            // Define a rotação da luz baseada na direção
            directionalLight.transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }
}
