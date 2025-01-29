using UnityEngine;

public class RescueTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Colidiu");
        if (collider != null)
        {
            Debug.Log("diferente de nulo");

            if (!collider.CompareTag("Player")) return;
            Debug.Log("é player");

            GameManager.instance.MoreOneTargetSafe();
            Debug.Log("mais um salvo");

            Destroy(GetComponentInParent<CelestialBodyHUD>().distanceText); // apaga texto
            Destroy(transform.parent.gameObject); // apaga planeta
        }

    }
}
