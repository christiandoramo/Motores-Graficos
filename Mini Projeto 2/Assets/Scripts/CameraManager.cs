using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[CreateAssetMenu(fileName = "cameraManager", menuName = "Managers/CameraManager")]
public class CameraManager : ScriptableObject
{

    public Camera camera1; // principal
    public Camera camera2; // secundária

    public Transform spaceshipTransform1;
    public Transform spaceshipTransform2;

    public GameObject spaceship1;
    public GameObject spaceship2;

    public AudioListener audioListener1;
    public AudioListener audioListener2; // secundária

    public Vector3 closePosition1 = new Vector3(0, 0, 1f);
    public float smallFOV1 = 30f; // field of view perto

    public Vector3 farPosition1 = new Vector3(0, 0, 1.5f);
    public float bigFOV1 = 75f; // field of view longe

    public Vector3 closePosition2 = new Vector3(0, 0, 1f);
    public float smallFOV2 = 40f; // field of view perto

    public Vector3 farPosition2 = new Vector3(0, 0, 1.5f);
    public float bigFOV2 = 60f; // field of view longe

    private int fovModeCam1;
    private int fovModeCam2;

    public Transform spaceBody1;
    public Transform spaceBody2;

    public float maxSpeed1;
    public float currentSpeed1;

    public float maxSpeed2;
    public float currentSpeed2;

    public float speedThreshold = 10f;
    public void Initialize()
    {

        camera2.enabled = true;
        camera1.rect = new Rect(0f, 0f, 0.5f, 1f);
        camera2.enabled = true;
        camera2.rect = new Rect(0.5f, 0f, 0.5f, 1f);


        fovModeCam1 = 1;
        fovModeCam2 = 1;

        camera1.fieldOfView = smallFOV1;
        camera1.transform.position = spaceBody1.position - closePosition1;
        camera2.fieldOfView = smallFOV2;
        camera2.transform.position = spaceBody2.position - closePosition2;

}

public void UpdateCamera()
    {
        bool pressed1 = Input.GetKeyDown(KeyCode.Alpha1);
        bool pressed2 = Input.GetKeyDown(KeyCode.Alpha2);
        bool pressed9 = Input.GetKeyDown(KeyCode.Alpha9);
        bool pressed0 = Input.GetKeyDown(KeyCode.Alpha0);
        ToggleFovMode(pressed1, pressed2, pressed9, pressed0);
        UpdateDynamicCameraPosition();
        camera1.transform.LookAt(spaceBody1);
        camera2.transform.LookAt(spaceBody2);
    }

    void UpdateDynamicCameraPosition()
    {
        if (currentSpeed1 > speedThreshold)
        {
            camera1.fieldOfView = Mathf.Lerp(camera1.fieldOfView, smallFOV1, Time.deltaTime);
            camera1.transform.position = Vector3.Lerp(
                spaceBody1.position + spaceBody1.rotation * -closePosition1,
                spaceBody1.position + spaceBody1.rotation * -farPosition1,
                Time.deltaTime
            );
        }
        else
        {
            camera1.fieldOfView = Mathf.Lerp(camera1.fieldOfView, bigFOV1, Time.deltaTime);
            camera1.transform.position = Vector3.Lerp(
                spaceBody1.position + spaceBody1.rotation * -farPosition1,
                spaceBody1.position + spaceBody1.rotation * -closePosition1,
                Time.deltaTime
            );
        }

        if (currentSpeed2 > speedThreshold)
        {
            camera2.fieldOfView = Mathf.Lerp(camera2.fieldOfView, smallFOV2, Time.deltaTime);
            camera2.transform.position = Vector3.Lerp(
                spaceBody2.position + spaceBody2.rotation * -closePosition2,
                spaceBody2.position + spaceBody2.rotation * -farPosition2,
                Time.deltaTime
            );
        }
        else
        {
            camera2.fieldOfView = Mathf.Lerp(camera2.fieldOfView, bigFOV2, Time.deltaTime);
            camera2.transform.position = Vector3.Lerp(
                spaceBody2.position + spaceBody2.rotation * -farPosition2,
                spaceBody2.position + spaceBody2.rotation * -closePosition2,
                Time.deltaTime
            );
        }

        // Certifique-se de que a câmera esteja sempre "olhando" para o objeto correto
        camera1.transform.LookAt(spaceBody1);
        camera2.transform.LookAt(spaceBody2);
    }


    public void ToggleFovMode(bool toggleToClose1, bool toggleToFar1, bool toggleToClose2, bool toggleToFar2) // podia ser um inteiro 1 ou 2 de entrada
    {
        if (toggleToClose1 && fovModeCam1 != 1)
        {
            fovModeCam1 = 1;
            camera1.fieldOfView = smallFOV1;
            camera1.transform.position = spaceBody1.position - closePosition1;
        }
        else if (toggleToFar1 && fovModeCam1 != 2)
        {
            fovModeCam1 = 2;

            camera1.fieldOfView = bigFOV1; // fovMaior
            camera1.transform.position = spaceBody1.position - farPosition1; ;
        }

        if (toggleToClose2 && fovModeCam2 != 1)
        {
            fovModeCam2 = 1;
            camera2.fieldOfView = smallFOV2;
            camera2.transform.position = spaceBody2.position - closePosition2;
        }
        else if (toggleToFar2 && fovModeCam2 != 2)
        {
            fovModeCam2 = 2;
            camera2.fieldOfView = bigFOV2;
            camera2.transform.position = spaceBody2.position - farPosition2;
        }
    }
}
