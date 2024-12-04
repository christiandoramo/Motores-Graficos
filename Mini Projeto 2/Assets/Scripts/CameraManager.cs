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

    public Vector3 closePosition1 = new Vector3(0, 0, .5f);
    public float smallFOV1 = 30f; // field of view perto

    public Vector3 farPosition1 = new Vector3(0, 0, 3f);
    public float bigFOV1 = 75f; // field of view longe

    public Vector3 closePosition2 = new Vector3(0, 0,.5f);
    public float smallFOV2 = 40f; // field of view perto

    public Vector3 farPosition2 = new Vector3(0, 0, 3f);
    public float bigFOV2 = 60f; // field of view longe

    private int modeCam1;
    private int modeCam2;

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


        modeCam1 = 3;
        modeCam2 = 3;

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

        bool pressed3 = Input.GetKeyDown(KeyCode.Alpha3);
        bool pressed8 = Input.GetKeyDown(KeyCode.Alpha8);


        ToggleFovMode(pressed1, pressed2, pressed9, pressed0, pressed3, pressed8);


        UpdateDynamicCameraPosition();
        camera1.transform.LookAt(spaceBody1);
        camera2.transform.LookAt(spaceBody2);
    }

    void UpdateDynamicCameraPosition()
    {
        if (currentSpeed1 > speedThreshold && modeCam1 == 3) // modo de cameralivre
        {
            camera1.fieldOfView = Mathf.Lerp(camera1.fieldOfView, smallFOV1, Time.deltaTime); // vai suavemente pra fov menor (zoom in)
            camera1.transform.position = Vector3.Lerp(  // vai suavemente pra distancia menor (se aproxima)
                spaceBody1.position + spaceBody1.rotation * -farPosition1,
                spaceBody1.position + spaceBody1.rotation * -closePosition1,
                Time.deltaTime
            );
        }
        else if (modeCam1 == 3)
        {
            camera1.fieldOfView = Mathf.Lerp(camera1.fieldOfView, bigFOV1, Time.deltaTime);
            camera1.transform.position = Vector3.Lerp( // se distancia suavemente
                spaceBody1.position + spaceBody1.rotation * -closePosition1,
                spaceBody1.position + spaceBody1.rotation * -farPosition1,
                Time.deltaTime
            );
        }

        if (currentSpeed2 > speedThreshold && modeCam2 == 3)
        {
            camera2.fieldOfView = Mathf.Lerp(camera2.fieldOfView, smallFOV2, Time.deltaTime);
            camera2.transform.position = Vector3.Lerp(
                spaceBody2.position + spaceBody2.rotation * -farPosition2,
                spaceBody2.position + spaceBody2.rotation * -closePosition2,
                Time.deltaTime
            );
        }
        else if (modeCam2 == 3)
        {
            camera2.fieldOfView = Mathf.Lerp(camera2.fieldOfView, bigFOV2, Time.deltaTime);
            camera2.transform.position = Vector3.Lerp(
                spaceBody2.position + spaceBody2.rotation * -closePosition2,
                spaceBody2.position + spaceBody2.rotation * -farPosition2,
                Time.deltaTime
            );
        }

        // Certifique-se de que a câmera esteja sempre "olhando" para o objeto correto
        camera1.transform.LookAt(spaceBody1);
        camera2.transform.LookAt(spaceBody2);
    }

    /// <summary>
    /// Troca a camera e fov para longe e fechado (1, 9), 3 muda pra camera livre - nave 1
    ///  Troca a camera e fov para perto e aberto (2, 0), 8 muda pra camera livre - nave 2
    /// </summary>
    /// <param name="toggleToClose1"></param>
    /// <param name="toggleToFar1"></param>
    /// <param name="toggleToClose2"></param>
    /// <param name="toggleToFar2"></param>
    /// <param name="toggleTofreeModeCam1"></param>
    /// <param name="toggleTofreeModeCam2"></param>
    public void ToggleFovMode(bool toggleToClose1, bool toggleToFar1, bool toggleToClose2, bool toggleToFar2, bool toggleTofreeModeCam1, bool toggleTofreeModeCam2) // podia ser um inteiro 1 ou 2 de entrada
    {
        // camera 1
        if (toggleTofreeModeCam1 && modeCam1 != 3)
        {
            modeCam1 = 3;
            //camera1.fieldOfView = bigFOV1; 
            //camera1.transform.position = spaceBody1.position - farPosition1;
        }
        else if (toggleToClose1 && modeCam1 != 1)
        {
            modeCam1 = 1;
            camera1.fieldOfView = smallFOV1; // zoom in
            camera1.transform.position = spaceBody1.position + spaceBody1.rotation * -farPosition1; // se afasta
        }
        else if (toggleToFar1 && modeCam1 != 2)
        {
            modeCam1 = 2;

            camera1.fieldOfView = bigFOV1; // fovMaior - zoom out
            camera1.transform.position = spaceBody1.position + spaceBody1.rotation * -closePosition1;  // se aproxima
        }
        // camera 2
        if (toggleTofreeModeCam2 && modeCam2 != 3)
        {
            modeCam2 = 3;
            //camera2.fieldOfView = bigFOV2;
            //camera2.transform.position = spaceBody2.position - farPosition2;
        }
        else if (toggleToClose2 && modeCam2 != 1)
        {
            modeCam2 = 1;
            camera2.fieldOfView = smallFOV2; // zoom in
            camera2.transform.position = spaceBody2.position + spaceBody2.rotation * -farPosition2; // se afasta
        }
        else if (toggleToFar2 && modeCam2 != 2)
        {
            modeCam2 = 2;
            camera2.fieldOfView = bigFOV2; // zoom out
            camera2.transform.position = spaceBody2.position + spaceBody2.rotation * -closePosition2; // se aproxima
        }
    }
}
