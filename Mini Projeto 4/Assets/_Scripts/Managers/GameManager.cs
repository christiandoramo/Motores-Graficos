using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{

    public CameraManager cameraManager;
    private SpaceshipController spaceshipController1;

    public GameObject endPanel;

    public int rescueTargetSafe = 0;
    public TextMeshProUGUI recuesCounterUI;
    public int totalToRescue = 0;

    public static GameManager instance;
    //void Start()
    //{
    //    if (instance == null)
    //    {
    //        instance = this; // definindo a instancia do objeto como estática para ser acessado como um
    //                         // singleton em outros arquivos
    //    }
    //    totalToRescue = GameObject.FindGameObjectsWithTag("CelestialBody").Length;
    //}
    //void Update()
    //{
    //    if (rescueTargetSafe >= totalToRescue)
    //    {
    //        EndGame();
    //    }
    //}

    public void MoreOneTargetSafe()
    {
        rescueTargetSafe++;
        recuesCounterUI.text = $"{rescueTargetSafe}/{totalToRescue}";
    }
    private void EndGame()
    {
        Time.timeScale = 0; // pausa jogo
        if (endPanel != null)
            endPanel.SetActive(true);
    }



    /// <summary>
    /// private SpaceshipController spaceshipController2;
    /// </summary>
    //void Start()
    //{
    //    if (cameraManager != null) cameraManager = Instantiate(cameraManager);
    //    else Destroy(cameraManager);

    //    cameraManager.camera1 = Camera.main; // camera que tem tag Main
    //    //cameraManager.camera2 = FindSecondaryCamera(); // camera secundaria - Untagged
    //    //cameraManager.spaceshipTransform2 = cameraManager.camera2.GetComponentInParent<Transform>(); // o pai é o player 2

    //    //spaceshipController2 = cameraManager.camera2.GetComponentInParent<SpaceshipController>();


    //    cameraManager.spaceship1 = GameObject.Find("Environment/Player");

    //    // cameraManager.spaceship2 = GameObject.Find("Spaceship2");

    //    cameraManager.spaceBody1 = GameObject.Find("Environment/Player/ShipBody").transform;
    //    cameraManager.spaceshipTransform1 = cameraManager.spaceBody1.transform;// o pai é o player 1
    //    spaceshipController1 = cameraManager.spaceship1.GetComponentInParent<SpaceshipController>();

    //    // cameraManager.spaceBody2 = GameObject.Find("Spaceship2/Sphere").transform;

    //    //cameraManager.currentSpeed1 = spaceshipController1.currentSpeed;
    //    cameraManager.maxSpeed1 = spaceshipController1.boostedSpeed;

    //    //cameraManager.currentSpeed2 = spaceshipController2.currentSpeed;
    //    //cameraManager.maxSpeed2 = spaceshipController2.boostedSpeed;

    //    cameraManager.Initialize();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (cameraManager == null) return;
    //    cameraManager.currentSpeed1 = cameraManager.spaceship1.GetComponent<SpaceshipController>().currentSpeed;
    //    //cameraManager.currentSpeed2 = cameraManager.spaceship2.GetComponent<SpaceshipController>().currentSpeed;
    //    cameraManager.UpdateCamera();
    //}

    ////private Camera FindSecondaryCamera()
    //{
    //    foreach (Camera camera in Camera.allCameras)
    //    {
    //        if (camera.CompareTag("Untagged"))
    //            return camera;
    //    }
    //    Debug.LogError("Nenhuma câmera secundária encontrada!");
    //    return null;
    //}
}
