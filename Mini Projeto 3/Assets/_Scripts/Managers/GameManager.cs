using UnityEngine;
public class GameManager : MonoBehaviour
{

    public CameraManager cameraManager;
    private SpaceshipController spaceshipController1;
    /// <summary>
    /// private SpaceshipController spaceshipController2;
    /// </summary>
    void Start()
    {
        if (cameraManager != null) cameraManager = Instantiate(cameraManager);
        else Destroy(cameraManager);

        cameraManager.camera1 = Camera.main; // camera que tem tag Main
        //cameraManager.camera2 = FindSecondaryCamera(); // camera secundaria - Untagged
        //cameraManager.spaceshipTransform2 = cameraManager.camera2.GetComponentInParent<Transform>(); // o pai � o player 2

        //spaceshipController2 = cameraManager.camera2.GetComponentInParent<SpaceshipController>();


        cameraManager.spaceship1 = GameObject.Find("Environment/Player");

        // cameraManager.spaceship2 = GameObject.Find("Spaceship2");

        cameraManager.spaceBody1 = GameObject.Find("Environment/Player/ShipBody").transform;
        cameraManager.spaceshipTransform1 = cameraManager.spaceBody1.transform;// o pai � o player 1
        spaceshipController1 = cameraManager.spaceship1.GetComponentInParent<SpaceshipController>();

        // cameraManager.spaceBody2 = GameObject.Find("Spaceship2/Sphere").transform;

        //cameraManager.currentSpeed1 = spaceshipController1.currentSpeed;
        cameraManager.maxSpeed1 = spaceshipController1.boostedSpeed;

        //cameraManager.currentSpeed2 = spaceshipController2.currentSpeed;
        //cameraManager.maxSpeed2 = spaceshipController2.boostedSpeed;

        cameraManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraManager == null) return;
        cameraManager.currentSpeed1 = cameraManager.spaceship1.GetComponent<SpaceshipController>().currentSpeed;
        //cameraManager.currentSpeed2 = cameraManager.spaceship2.GetComponent<SpaceshipController>().currentSpeed;
        cameraManager.UpdateCamera();
    }

    //private Camera FindSecondaryCamera()
    //{
    //    foreach (Camera camera in Camera.allCameras)
    //    {
    //        if (camera.CompareTag("Untagged"))
    //            return camera;
    //    }
    //    Debug.LogError("Nenhuma c�mera secund�ria encontrada!");
    //    return null;
    //}
}
