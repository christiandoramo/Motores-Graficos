using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MotionBlur : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private UnityEngine.Rendering.PostProcessing.MotionBlur motionBlur;

    public Camera targetCamera;
    private Quaternion previousRotation;

    void Start()
    {
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out motionBlur);
        }

        if (targetCamera != null)
        {
            previousRotation = targetCamera.transform.rotation;
        }
    }

    void Update()
    {
        if (motionBlur != null && targetCamera != null)
        {
            // Calcula a velocidade de rotação da câmera
            float cameraRotationSpeed = Quaternion.Angle(previousRotation, targetCamera.transform.rotation) / Time.deltaTime;

            // Ajusta a intensidade do blur com base na velocidade da rotação
            motionBlur.shutterAngle.value = Mathf.Lerp(0f, 300f, cameraRotationSpeed / 100f); // Ajuste "100f" para controle da sensibilidade

            // Atualiza a rotação anterior
            previousRotation = targetCamera.transform.rotation;
        }
    }
}
