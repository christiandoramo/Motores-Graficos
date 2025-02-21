using Unity.VisualScripting;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private BoxCollider triggerCollider;

    private GameObject player;
    void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if (player != null)
            FindPlayer();
    }
    void FindPlayer()
    {
        if (player.GetComponent<Rigidbody>().useGravity == false)
        {
            return;
        }
        Collider[] colliders = Physics.OverlapBox(triggerCollider.bounds.center, triggerCollider.bounds.extents, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {

        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<Rigidbody>().useGravity = false;
            player = other.collider.gameObject;
            Debug.Log("Player está na escada");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<Rigidbody>().useGravity = true;
            player = null;
        }
    }
}
