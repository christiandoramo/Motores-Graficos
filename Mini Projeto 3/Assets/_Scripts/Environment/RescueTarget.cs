using UnityEngine;

public class RescueTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter(Collider collider)
    {
        if (collider != null)
        {
            GameManager.instance.MoreOneTargetSafe();
            Destroy(transform.parent.gameObject);
            //Destroy(transform.GetChild(0).gameObject); // destruindo lampada
            //Destroy(gameObject);
        }

    }
}
