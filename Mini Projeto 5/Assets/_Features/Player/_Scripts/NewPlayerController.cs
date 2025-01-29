using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    [Header("Valor de Inputs")]
    public InputActionReference move;
    public InputActionReference look;
    public InputActionReference roll;
    public InputActionReference accelerate;
    public InputActionReference interact;
    [Tooltip("Input de movimento - cima baixo")] float verticalMove;
    [Tooltip("Input de movimento - esquerda direita")] float horizontalMove;
    [Tooltip("Input de movimento - vira para esquerda direita")] float mouseInputX;
    [Tooltip("Input de movimento - vira para cima baixo")] float mouseInputY;
    [Tooltip("Input de movimento - frente ré")] float rollInput;
    [Tooltip("Input de movimento - frente ré")] float accelerateInput;



    [Header("Física do movimento")]
    [SerializeField] float speedMove = 0.5f;
    [SerializeField] float speedLook = .005f;
    [SerializeField] float speedRoll = .025f;
    [SerializeField] float accelerationForce = .5f;
    [SerializeField] Rigidbody rb;

    [Header("Efeitos")]
    [SerializeField] private Animator propulsionAnimator;

    [Header("Multiplayer...")]
    PlayerHUD playerHUD;
    public bool isActivated;
    [SerializeField] public Camera playerCamera;

    void Start()
    {
        playerHUD = GetComponent<PlayerHUD>();
        if (playerHUD.playerNumber == 1) isActivated = true;
    }

    private void Update()
    {
        if (!isActivated) return;
        // move - frente-tras, esquerda-direita
        Vector2 moveDirection = move.action.ReadValue<Vector2>();
        horizontalMove = moveDirection.x;
        verticalMove = moveDirection.y;

        // cima baixo
        rollInput = roll.action.ReadValue<float>();

        // input mouse baixo-cima, esquerda-direita
        Vector2 lookDirection = look.action.ReadValue<Vector2>();
        mouseInputX = lookDirection.x;
        mouseInputY = lookDirection.y;

        accelerateInput = accelerate.action.ReadValue<float>();

        propulsionAnimator.SetFloat("Propulsion", verticalMove);
        propulsionAnimator.SetFloat("Acelleration", accelerateInput);
    }

    void FixedUpdate()
    {
        float currentMoveSpeed = speedMove * verticalMove;
        float targetMoveSpeed = Mathf.Clamp(currentMoveSpeed + accelerateInput * (currentMoveSpeed * (1 + accelerationForce)), 0, accelerationForce);
        float currentThrust = Mathf.Lerp(currentMoveSpeed, targetMoveSpeed, Time.fixedDeltaTime * accelerationForce);


        rb.AddForce(currentThrust * rb.transform.TransformDirection(Vector3.forward), ForceMode.VelocityChange);
        rb.AddForce(horizontalMove * speedMove * rb.transform.TransformDirection(Vector3.right), ForceMode.VelocityChange);
        rb.AddTorque(-mouseInputY * speedLook * rb.transform.right, ForceMode.VelocityChange);
        rb.AddTorque(mouseInputX * speedLook * rb.transform.up, ForceMode.VelocityChange);

        rb.AddTorque(rollInput * speedRoll * rb.transform.forward, ForceMode.VelocityChange);

    }
    private void OnEnable()
    {
        interact.action.started += Interact;
    }
    private void OnDisable()
    {
        interact.action.started -= Interact;

    }
    private void Interact(InputAction.CallbackContext ctx)
    {
        if (playerHUD.players.Length < 2) return;
        isActivated = !isActivated;
        if (isActivated)
        {
            playerCamera.gameObject.SetActive(true);
            Debug.Log("Player Ativado: " + playerHUD.playerNumber);
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
        }

    }
}