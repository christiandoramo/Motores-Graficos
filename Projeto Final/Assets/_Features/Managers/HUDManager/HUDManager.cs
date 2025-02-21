
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI woodCounter;
    public TextMeshProUGUI waterCounter;
    public TextMeshProUGUI oilCounter;
    public TextMeshProUGUI loadCounter;


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

    public void UpdateLoadCount(float inCalderon, int total)
    {
        loadCounter.text = $"Load: {inCalderon}/{total}";

    }

}
