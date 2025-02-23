using UnityEngine;
using TMPro;
using System.Linq;

public class SafePointHUD : MonoBehaviour
{
    public string title;
    [SerializeField] private TextMeshProUGUI safePointUI;

    public Transform player;
    private Canvas selfCanvas;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        safePointUI = GameObject.FindWithTag("SafePointUI").GetComponent<TextMeshProUGUI>();

        selfCanvas = GetComponentInChildren<Canvas>();
        safePointUI.rectTransform.position = selfCanvas.transform.position;
        safePointUI.rectTransform.SetParent(selfCanvas.transform);
    }

    void LateUpdate()
    {
        UpdateNextSafePointInfo();
    }
    public void UpdateNextSafePointInfo()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        string displayText = $"{title}\n{distance:F2}";
        safePointUI.text = displayText;

        // atualizando distancia
        Vector3 screenPosition = GameManager.instance.activeCamera.WorldToScreenPoint(transform.position);
        if (screenPosition.z > 0)
        {
            safePointUI.transform.position = screenPosition;
            safePointUI.gameObject.SetActive(true);
        }
        else
        {
            safePointUI.gameObject.SetActive(false);
        }
    }

}
