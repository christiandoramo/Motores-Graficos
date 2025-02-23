using UnityEngine;
using UnityEngine.InputSystem;

public class InputsService : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference brake;

    [SerializeField] private InputActionReference look;
    [SerializeField] private InputActionReference accelerate;
    [SerializeField] private InputActionReference gather;
    [SerializeField] private InputActionReference toggleDrive;

    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private bool isActivated = true;
    [Tooltip("cima baixo")] public float zMove;
    [Tooltip("esquerda direita")] public float xMove;
    [Tooltip("freiar")] public float brakeInput;
    [Tooltip("vira para esquerda direita")] public float mouseInputX;
    [Tooltip("vira para cima baixo")] public float mouseInputY;
    [Tooltip("aceleracao")] public float accelerateInput;

    private PlayerMove playerMove;
    private PlayerGather playerGather;
    private PlayerCollect playerCollect;
    private ShipWheel shipWheel;
    private PlayerAttack playerAttack;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerGather = GetComponent<PlayerGather>();
        playerCollect = GetComponent<PlayerCollect>();
        playerAttack = GetComponent<PlayerAttack>();
        shipWheel = GameObject.FindWithTag("ShipWheel").GetComponent<ShipWheel>();
    }
    void Update()
    {
        if (!isActivated) return;
        if (!GameManager.instance.isDriving)
        {
            PlayerCharInputs();
        }
        else if (GameManager.instance.isDriving)
        {
            CarInputs();
        }
    }

    private void CarInputs()
    {
        Vector2 moveDirection = move.action.ReadValue<Vector2>();
        xMove = moveDirection.x;
        zMove = moveDirection.y;


        Vector2 lookDirection = look.action.ReadValue<Vector2>();
        mouseInputX = lookDirection.x;
        mouseInputY = lookDirection.y;

        accelerateInput = accelerate.action.ReadValue<float>();
        brakeInput = brake.action.ReadValue<float>();

    }
    private void PlayerCharInputs()
    {
        Vector2 moveDirection = move.action.ReadValue<Vector2>();
        xMove = moveDirection.x;
        zMove = moveDirection.y;


        Vector2 lookDirection = look.action.ReadValue<Vector2>();
        mouseInputX = lookDirection.x;
        mouseInputY = lookDirection.y;

        accelerateInput = accelerate.action.ReadValue<float>();
    }
    private void OnEnable()
    {
        jump.action.started += Jump;
        gather.action.started += Gather;
        gather.action.started += Collect;
        toggleDrive.action.started += ToggleDriveMode;
        attack.action.started += Attack;

    }
    private void OnDisable()
    {
        jump.action.started -= Jump;
        gather.action.started -= Gather;
        gather.action.started -= Collect;
        toggleDrive.action.started -= ToggleDriveMode;
        attack.action.started -= Attack;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        playerMove.Jump();
    }

    private void Gather(InputAction.CallbackContext ctx)
    {
        playerGather.Gather();
    }
    private void Collect(InputAction.CallbackContext ctx)
    {
        playerCollect.Collect();
    }
    private void ToggleDriveMode(InputAction.CallbackContext ctx)
    {
        shipWheel.IsPlayerInsideTrigger();
    }
    private void Attack(InputAction.CallbackContext ctx)
    {
        playerAttack.TorchAttack();
    }
}
