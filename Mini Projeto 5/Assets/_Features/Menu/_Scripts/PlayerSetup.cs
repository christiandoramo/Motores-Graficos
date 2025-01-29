using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSetup : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_Dropdown playerCountDropdown;
    [SerializeField] private GameObject player1Input;
    [SerializeField] private GameObject player2Input;
    [SerializeField] private Button confirmButton;

    private static string[] playerNames;

    void Start()
    {
        UpdateInputFields();

        playerCountDropdown.onValueChanged.AddListener(delegate { UpdateInputFields(); });
        confirmButton.onClick.AddListener(ConfirmPlayers);
    }

    private void UpdateInputFields()
    {
        // mostraando  os campos de input baseado no número de jogadores
        int playerCount = playerCountDropdown.value + 1; // 1 para "1 Player", 2 para "2 Players"
        player1Input.SetActive(true);
        player2Input.SetActive(playerCount == 2);
    }

    private void ConfirmPlayers()
    {
        TMP_InputField input1 = player1Input.GetComponent<TMP_InputField>();
        TMP_InputField input2 = player2Input.GetComponent<TMP_InputField>();

        if (input1.text == "")
        {
            Debug.Log("Insira o nome do player 1");
            return;
        }
        else if (player2Input.activeSelf && input2.text == "")
        {
            Debug.Log("Insira o nome do player 2");
            return;
        }
        else if (player2Input.activeSelf && input2.text != "")
        {
            playerNames = new string[2];
            playerNames[0] = input1.text;
            playerNames[1] = input2.text;
            SceneManager.LoadScene("GameScene");
        }
        else if (!player2Input.activeSelf)
        {
            playerNames = new string[1];
            playerNames[0] = input1.text;
            SceneManager.LoadScene("GameScene");
        }
    }

    public static string[] GetPlayerNames()
    {
        return playerNames;
    }
}
