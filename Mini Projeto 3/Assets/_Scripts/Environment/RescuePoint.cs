using System.Collections;
using UnityEngine;

public class RescuePoint : MonoBehaviour
{
    public Light flashlight;
    public bool flashlightOn;


    void Start()
    {
        if (flashlight == null) // Exemplo: ativa com a tecla F
        {
            flashlight = transform.GetChild(0).GetComponent<Light>();
        }
    }

    void Update()
    {
        HandleToggleLight();
    }

    void HandleToggleLight()
    {
        if (!flashlightOn) StartCoroutine(RescueFlashLightToggle());
    }

    private IEnumerator RescueFlashLightToggle()
    {
        if (!flashlightOn)
        {
            flashlightOn = true;
            flashlight.enabled = true;
            yield return new WaitForSeconds(.5f);
        }
        flashlight.enabled = false;
        yield return new WaitForSeconds(.5f);
        flashlightOn = false;
    }
}
