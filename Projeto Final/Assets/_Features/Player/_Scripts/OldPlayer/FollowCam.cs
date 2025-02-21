using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //public float yOffset, moveDelay;
    public float moveDelay;

    //public float sensitivity, rotationLimit, rotatioionDelay;
    //private float mouseX, mouseY;
    //private float rotX, rotY;
    [SerializeField] private Transform head;
    GameObject player;
    PlayerController controller;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
    }
    //private void Update()
    //{
    //    //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 0);
    //}
    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(controller.verticalRotation, player.transform.rotation.eulerAngles.y, 0);

        transform.position = head.position;
    }
}