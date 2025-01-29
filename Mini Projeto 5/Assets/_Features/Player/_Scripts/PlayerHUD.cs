using UnityEngine;
using TMPro;
using System.Linq;

public class PlayerHUD : MonoBehaviour
{

    [Header("Referências do Astro")]
    public string playerName;
    public int playerNumber;
    public NewPlayerController playerController;
    [SerializeField] private Transform playerBodyTransform;
    [SerializeField] public TextMeshProUGUI distanceText; // TextMeshPro no planeta

    [Header("Jogadores")]
    [SerializeField] public Transform[] players;
    [SerializeField] public Camera[] playerCameras;
    void Start()
    {
        playerController = GetComponentInChildren<NewPlayerController>();
        GameObject canvas = GameObject.FindWithTag("Canvas");
        if (canvas != null)
        {
            distanceText.transform.SetParent(canvas.GetComponent<RectTransform>(), false);
        }
        else
        {
            Debug.Log("canvas nulo");

        }

        Cursor.lockState = CursorLockMode.Locked;

    }


    private void Update()
    {
        if (playerController.isActivated) // não deve mostrar proprio nome e distancia
        {
            distanceText.text = "";
        }
        else
        {
            int otherPlayerNumber = (playerNumber % 2) + 1;
            float distance = Vector3.Distance(playerBodyTransform.position, players[otherPlayerNumber - 1].position);

            string displayText = $"{playerName}\n{distance:F2}";

            distanceText.text = displayText;
            UpdateTextPosition(otherPlayerNumber - 1);
        }
    }

    private void UpdateTextPosition(int playerIndex)
    {
        Vector3 screenPosition = playerCameras[playerIndex].WorldToScreenPoint(playerBodyTransform.position);

        if (screenPosition.z > 0)
        {
            distanceText.transform.position = screenPosition;
            distanceText.gameObject.SetActive(true);
        }
        else
        {
            distanceText.gameObject.SetActive(false);
        }
    }
}
