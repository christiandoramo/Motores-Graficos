using UnityEngine;

public class Bubble : MonoBehaviour
{
    void LateUpdate()
    {
        if (GameManager.instance.rm.load == 0)
        {
            Debug.Log("Explodiu bolha bubble");
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bolha atacou inimigo");
            other.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }
}
