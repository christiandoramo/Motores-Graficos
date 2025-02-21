using UnityEngine;

public class ShipWheel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private BoxCollider triggerCollider;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wagon;


    [SerializeField] private Camera carCamera;

    void Start()
    {
        wagon = GameObject.FindWithTag("Wagon");

        player = GameObject.FindWithTag("Player");
        playerCamera = Camera.main;
        carCamera = GameObject.FindWithTag("CarCamera").GetComponent<Camera>();
        carCamera.gameObject.SetActive(false);
        triggerCollider = GetComponent<BoxCollider>();
    }

    void ToggleDriveMode()
    {
        Debug.Log("ToggleDriveMode");
        // caldeiron.load não está sendo usado
        if (GameManager.instance.rm.load > 0 && GameManager.instance.isDriving == false)
        {
            player.GetComponent<Rigidbody>().isKinematic = true;
            player.transform.parent = gameObject.transform;
            carCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            GameManager.instance.isDriving = true;
        }
        else if (GameManager.instance.isDriving == true)
        {
            playerCamera.gameObject.SetActive(true);
            carCamera.gameObject.SetActive(false);
            GameManager.instance.isDriving = false;
            player.GetComponent<Rigidbody>().isKinematic = false;
            player.transform.parent = null;
        }
    }

    public void IsPlayerInsideTrigger()
    {
        Debug.Log("Apertou C");
        Collider[] colliders = Physics.OverlapBox(triggerCollider.bounds.center, triggerCollider.bounds.extents * 2f, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                ToggleDriveMode();
                return;
            }
        }
        // playerCamera.gameObject.SetActive(false);
        // carCamera.gameObject.SetActive(false);
        // GameManager.instance.isDriving = false;
    }
}
