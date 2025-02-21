using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float lookSpeed = 100f;
    //[SerializeField] private float jumpForce = 100f;
    [SerializeField] private float upDownRange = 80f;
    public Transform camHolder = null;

    [SerializeField] private GameObject head;


    public InputActionReference move;
    public InputActionReference look;
    public InputActionReference jump;
    public InputActionReference attack;
    public InputActionReference gather;



    private Vector2 moveDirection;
    private Vector2 lookDirection;
    public float verticalRotation;
    private float horizontalRotation = 0f;
    public Animator animator;

    public bool isPushing;
    public float currentSpeed;
    public bool isCarrying;
    public bool isGathering;



    public GatheringController gatheringController;


    private void Start()
    {
        gatheringController = GetComponent<GatheringController>();
        gatheringController.pc = this;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        // rb.interpolation = RigidbodyInterpolation.Interpolate;
    }
    private void Update()
    {
        if (isGathering) return;
        moveDirection = move.action.ReadValue<Vector2>();
        lookDirection = look.action.ReadValue<Vector2>();

        Vector3 localScale = transform.localScale;
        if (moveDirection.x > 0 && localScale.x < 0)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        else if (moveDirection.x < 0 && localScale.x > 0)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        horizontalRotation += lookDirection.x * lookSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, horizontalRotation, 0);
        verticalRotation -= lookDirection.y * lookSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);


        Vector3 moveDirection3D = new Vector3(moveDirection.x, 0, moveDirection.y).normalized;

        Vector3 move3D = moveDirection3D * moveSpeed * gatheringController.weightGatheringMultiplier * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(move3D));

        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);
        animator.SetBool("isPushing", isPushing);
        animator.SetFloat("currentSpeed", Mathf.Abs(moveDirection.x + moveDirection.y));
    }
    private void OnEnable()
    {
        attack.action.started += Attack;
        jump.action.started += Jump;

        gather.action.started += gatheringController.Gather;
    }
    private void OnDisable()
    {
        attack.action.started -= Attack;
        jump.action.started -= Jump;
        gather.action.started -= gatheringController.Gather;
    }

    /// <summary>
    /// Função responsável pelo attack
    /// </summary>
    private void Attack(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Atacou: ");
    }
    private void Jump(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Pulou: ");
    }
}