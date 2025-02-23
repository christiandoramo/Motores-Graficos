using System.Linq;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMove : MonoBehaviour
{
    [Header("Camera config")][SerializeField] float camLookSpeed;

    [Header("Fisica do movimento")]
    public float sp = 10;
    public float spMax = 10;
    private float staminaHasBeenUsedCounter;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float lookSpeed = .005f;
    [SerializeField] private float upDownRange = 80f;
    [SerializeField]
    float accelerationForce = .5f;
    [SerializeField] private PlayerGather pg;
    [SerializeField] Rigidbody rb;

    [Header("Meu sistema de Inputs")]
    [SerializeField] InputsService inputs;

    internal bool isGathering;
    internal bool isCarrying;
    internal bool isPushing;
    internal bool isOnStairs;
    [SerializeField] internal Animator animator;
    internal float currentSpeed;
    float verticalRotation = 0;

    [Header("Jump Config")]
    [SerializeField] float jumpForce;
    bool isGrounded;
    [SerializeField] Vector3 isGroundedBoxSize;
    [SerializeField] Transform feetTransform;
    [SerializeField] LayerMask groundMask;
    int jumpsEnabled = 2;
    private Transform camHolderTransform;
    void Start()
    {
        camHolderTransform = Camera.main.transform.parent;
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isDriving) return;
        HandleStamina();
        Move();

    }
    private void Move()
    {
        verticalRotation -= inputs.mouseInputY * camLookSpeed * Time.fixedDeltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        camHolderTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        if (isGathering) return;

        float currentMoveSpeed = moveSpeed * inputs.zMove;
        float targetMoveSpeed = Mathf.Clamp(currentMoveSpeed + inputs.accelerateInput * (currentMoveSpeed * (1 + accelerationForce)), 0, accelerationForce);
        float currentThrust = Mathf.Lerp(currentMoveSpeed, targetMoveSpeed, Time.fixedDeltaTime * accelerationForce);

        rb.AddForce(pg.weightGatheringMultiplier * currentThrust * rb.transform.TransformDirection(Vector3.forward), ForceMode.VelocityChange);
        rb.AddForce(pg.weightGatheringMultiplier * inputs.xMove * moveSpeed * rb.transform.TransformDirection(Vector3.right), ForceMode.VelocityChange);

        rb.AddTorque(inputs.mouseInputX * lookSpeed * rb.transform.up, ForceMode.VelocityChange); // giro para esquerda e direita com mouse
    }
    public void Jump() // tem pulo duplo
    {
        isGrounded = IsGroundedBoxCreate(groundMask, feetTransform.position, isGroundedBoxSize);
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsEnabled = 1;
            isGrounded = false;
            Debug.Log("Pulou");
        }
        else
        {
            if (jumpsEnabled < 1)
            {
                Debug.Log("Não pode mais pular");
                return;
            }
            else
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // anulando gravidade
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpsEnabled = 0;
                Debug.Log("Pulou");
            }
        }
    }

    private bool IsGroundedBoxCreate(LayerMask targetMask, Vector3 origin, Vector3 boxSize) // ataque "Realista" pode errar ou acertar
    {
        Collider[] colliders = Physics.OverlapBox(origin, boxSize * .5f, Quaternion.identity, targetMask);
        Collider collider = colliders.FirstOrDefault((collider) => collider != null);
        return collider != null;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Stairs"))
        {
            isOnStairs = true;
            rb.useGravity = false;
            Debug.Log("Player está na escada");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Stairs"))
        {
            isOnStairs = false;
            rb.useGravity = true;
            Debug.Log("Player está fora da escada");
        }
    }

    private void HandleStamina()
    {
        if (inputs.zMove != 0 && inputs.accelerateInput > 0 && sp > 0) // caso acelera load da aceleração entra em cooldown
        {
            sp -= Time.fixedDeltaTime;
            staminaHasBeenUsedCounter = 3f;
            GameManager.instance.hudManager.UpdateSPCount(sp, (int)spMax);
        }

        if (sp < spMax && staminaHasBeenUsedCounter <= 0)
        {
            if (staminaHasBeenUsedCounter < 2f && sp < spMax)
            {
                sp += Time.fixedDeltaTime; // fica aumentando a cada frame da unity em 0.0000... float dando 1 a cada seg
                if (sp >= spMax)
                {
                    sp = spMax;
                }
            }
            GameManager.instance.hudManager.UpdateSPCount(sp, (int)spMax);
        }
        else if (staminaHasBeenUsedCounter >= 0 && staminaHasBeenUsedCounter <= 2f) // não foi usada em 2 segundos
        {
            staminaHasBeenUsedCounter -= Time.fixedDeltaTime;
            if (staminaHasBeenUsedCounter <= 0f)
                staminaHasBeenUsedCounter = 0f;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(feetTransform.transform.position, isGroundedBoxSize);
    }
}
