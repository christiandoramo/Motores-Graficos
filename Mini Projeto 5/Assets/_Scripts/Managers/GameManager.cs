using System.Linq;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public GameObject playerPrefab;
    private SpaceshipController spaceshipController1;

    public GameObject endPanel;

    public int rescueTargetSafe = 0;
    public TextMeshProUGUI recuesCounterUI;
    public int totalToRescue = 0;

    public static GameManager instance;



    void Start()
    {
        if (instance == null)
        {
            instance = this; // definindo a instancia do objeto como est�tica para ser acessado como um
                             // singleton em outros arquivos
        }
        totalToRescue = GameObject.FindGameObjectsWithTag("CelestialBody").Length;

        string[] playerNames = PlayerSetup.GetPlayerNames();
        int playersNumber = playerNames.Length;

        if (playersNumber == 2)
        {
            GameObject player1 = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            GameObject realPlayer1 = player1.transform.GetChild(0).gameObject;
            PlayerHUD realPlayer1HUD = realPlayer1.GetComponent<PlayerHUD>();
            realPlayer1HUD.playerName = playerNames[0];
            realPlayer1HUD.playerNumber = 1;


            GameObject player2 = Instantiate(playerPrefab, Vector3.forward * 10f, Quaternion.identity);
            GameObject realPlayer2 = player2.transform.GetChild(0).gameObject;
            PlayerHUD realPlayer2HUD = realPlayer2.GetComponent<PlayerHUD>();
            realPlayer2HUD.playerName = playerNames[1];
            realPlayer2HUD.playerNumber = 2;

            Transform[] transforms = GameObject.FindGameObjectsWithTag("Player")
                                   .Select(player => player.transform)
                                   .ToArray();

            realPlayer1HUD.players = transforms;
            realPlayer2HUD.players = transforms;

            Camera[] cameras = transforms.Select(player => player.GetComponentInChildren<Camera>())
                                      .ToArray();

            realPlayer1HUD.playerCameras = cameras;
            realPlayer2HUD.playerCameras = cameras;

            CelestialBodyHUD[] celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody")
            .Select(player => player.GetComponent<CelestialBodyHUD>())
                           .ToArray();
            foreach (CelestialBodyHUD celestialBody in celestialBodies)
            {
                celestialBody.players = transforms;
                celestialBody.playerCameras = cameras;
            }
            realPlayer2.GetComponent<NewPlayerController>().playerCamera.gameObject.SetActive(false);

            realPlayer1.GetComponent<NewPlayerController>().playerCamera.gameObject.SetActive(true);
            realPlayer1.GetComponent<NewPlayerController>().isActivated = true;



            // gestao das cameras
        }
        else if (playersNumber == 1)
        {
            GameObject player1 = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            GameObject realPlayer1 = player1.transform.GetChild(0).gameObject;
            PlayerHUD realPlayer1HUD = realPlayer1.GetComponent<PlayerHUD>();
            realPlayer1HUD.playerName = playerNames[0];
            realPlayer1HUD.playerNumber = 1;
            realPlayer1.GetComponent<NewPlayerController>().playerCamera.gameObject.SetActive(true);
            realPlayer1.GetComponent<NewPlayerController>().isActivated = true;

            Transform[] transforms = GameObject.FindGameObjectsWithTag("Player")
                                   .Select(player => player.transform)
                                   .ToArray();

            Camera[] cameras = transforms.Select(player => player.GetComponentInChildren<Camera>())
                                                 .ToArray();
            CelestialBodyHUD[] celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody")
            .Select(player => player.GetComponent<CelestialBodyHUD>())
               .ToArray();
            foreach (CelestialBodyHUD celestialBody in celestialBodies)
            {
                celestialBody.players = transforms;
                celestialBody.playerCameras = cameras;
            }
        }
        else
        {
            Debug.Log("Nenhum jogador detectado");
        }
    }

    public void MoreOneTargetSafe()
    {
        rescueTargetSafe++;
        recuesCounterUI.text = $"{rescueTargetSafe}/{totalToRescue}";
        if (rescueTargetSafe == totalToRescue) EndGame();
    }
    private void EndGame()
    {
        Time.timeScale = 0; // pausa jogo
        if (endPanel != null)
            endPanel.SetActive(true);
    }


}
