using UnityEngine;

public class AlignDirectionalLightToCamera : MonoBehaviour
{
    public Light directionalLight; // A luz direcional
    public Camera targetCamera;    // A c�mera de refer�ncia

    void Update()
    {
        if (directionalLight != null && targetCamera != null)
        {
            // Calcula a dire��o da luz para a c�mera
            Vector3 directionToCamera = targetCamera.transform.position - directionalLight.transform.position;

            // Inverte a dire��o para simular luz vinda "de tr�s da c�mera"
            directionToCamera = -directionToCamera.normalized;

            // Define a rota��o da luz baseada na dire��o
            directionalLight.transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }
}
