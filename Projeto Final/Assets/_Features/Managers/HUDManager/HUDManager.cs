
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI woodCounter;
    public TextMeshProUGUI waterCounter;
    public TextMeshProUGUI oilCounter;
    [SerializeField] Slider hpBar;
    [SerializeField] TextMeshProUGUI hpCounter;

    [SerializeField] Slider spBar;
    [SerializeField] TextMeshProUGUI spCounter;

    [SerializeField] Slider loadBar;
    public TextMeshProUGUI loadCounter;


    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        hpBar.maxValue = player.GetComponent<PlayerHealth>().startingHealth;
        hpBar.value = hpBar.maxValue;
        hpCounter.text = $"{hpBar.value}/{hpBar.maxValue}";

        spBar.maxValue = player.GetComponent<PlayerMove>().spMax;
        spBar.value = spBar.maxValue;
        spCounter.text = $"{spBar.value}/{spBar.maxValue}";

        loadBar.maxValue = GameManager.instance.rm.maxLoad; // é charge, e não load... =)
        loadBar.value = 0;
        loadCounter.text = $"{loadBar.value}/{0}";
    }

    public void UpdateWoodCount(float inCalderon, int total)
    {
        woodCounter.text = $"Wood: {inCalderon}/{total}";
    }
    public void UpdateOilCount(float inCalderon, int total)
    {
        oilCounter.text = $"Oil: {inCalderon}/{total}";

    }
    public void UpdateWaterCount(float inCalderon, int total)
    {
        waterCounter.text = $"Water: {inCalderon}/{total}";

    }
    public void UpdateHPCount(float hp, int totalHp)
    {
        hpBar.value = hp;
        hpCounter.text = $"{hp}/{totalHp}";
    }

    public void UpdateSPCount(float sp, int totalSp)
    {
        spBar.value = sp;
        spCounter.text = $"{sp}/{totalSp}";
    }

    public void UpdateLoadCount(float load, int totalLoad)
    {
        loadBar.value = load;
        loadCounter.text = $"{load}/{totalLoad}";
    }
}
