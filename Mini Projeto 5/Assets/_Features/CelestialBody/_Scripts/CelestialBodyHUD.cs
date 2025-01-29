using UnityEngine;
using TMPro;
using System.Linq;

public class CelestialBodyHUD : MonoBehaviour
{
    [Header("Referências do Astro")]
    public string celestialBodyName;
    [SerializeField] private Transform celestialBodyTransform; 
    [SerializeField] public TextMeshProUGUI distanceText; 

    [Header("Jogadores")]
    public Transform[] players;
    public Camera[] playerCameras; 

    void Update()
    {
        bool anyPlayerActivated = false;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].gameObject.GetComponent<NewPlayerController>().isActivated)
            {
                anyPlayerActivated = true;

                // Calcula a distância e exibe o texto
                float distance = Vector3.Distance(celestialBodyTransform.position, players[i].position);
                string displayText = $"{celestialBodyName}\n{distance:F2}";

  
                if (playerCameras[i].isActiveAndEnabled)
                {
                    distanceText.text = displayText;
                    UpdateTextPosition(i);
                }
            }
        }

      
        if (!anyPlayerActivated)
        {
            distanceText.text = "";
        }
    }


    private void UpdateTextPosition(int playerIndex)
    {
        Vector3 screenPosition = playerCameras[playerIndex].WorldToScreenPoint(celestialBodyTransform.position);

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
