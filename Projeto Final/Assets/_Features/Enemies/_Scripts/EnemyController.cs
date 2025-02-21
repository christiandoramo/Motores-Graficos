using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2f;
    public GameObject player;
    // [SerializeField] float stopDistance = .05f; // distancia da colisão
    public bool isDying = false;


    public Rigidbody rb;
    public Animator animator;

    [SerializeField] private LayerMask playerColliderMask;

    private bool isMoving = true;
    private bool isAttacking = false;

    public Vector3 hitBoxSize = new(3, 3, 3);
    [SerializeField] private float coolDownTimer;
    [SerializeField] float torchCooldown;
    [SerializeField]
    LayerMask layerAttackMask;



   [Header("References")]
    [SerializeField] private Transform attackOriginPoint;
    [SerializeField] public GameObject attackPrefab;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject; // pega o player já na cena
    }
    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer <= torchCooldown && isAttacking == true)
        {
            isAttacking = false;
            animator.SetBool("IsAttacking", isAttacking);
        }

        FollowPlayer();
        if (coolDownTimer >= 60)
        {
            rb.linearVelocity = Vector3.zero;
            isAttacking = false;
            isMoving = true;
            animator.SetBool("IsMoving", isMoving);
            animator.SetBool("IsAttacking", isAttacking);

            coolDownTimer = 0;
        }
    }

    void FollowPlayer()
    {
        if (player == null) return;
        Collider[] colliders = Physics.OverlapBox(transform.position, hitBoxSize * .5f, Quaternion.identity, playerColliderMask);
        Collider collider = colliders.FirstOrDefault((collider) => collider != null);

        if (collider != null)
        {

            TorchAttack();

        }
        else if (isAttacking == false)
        {
            Move();
        }

    }

    private void Move()
    {
        isMoving = true;
        animator.SetBool("IsMoving", isMoving);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        transform.rotation = Quaternion.LookRotation(direction); // Rotacionar na direção do movimento
    }

    private void TorchAttack()
    {
        isMoving = false;
        animator.SetBool("IsMoving", isMoving);

        if (isAttacking == false)
        {
            animator.SetBool("IsAttacking", true);
        }
        isAttacking = true;
        if (coolDownTimer < torchCooldown) return;

        Vector3 direction = attackOriginPoint.forward;
        Vector3 attackArea = hitBoxSize;

        GameObject attackObj = Instantiate(attackPrefab, attackOriginPoint.position, Quaternion.identity);
        attackObj.GetComponent<HandsAttack>().Execute(direction, attackArea, layerAttackMask, Vector3.zero);

        coolDownTimer = 0f; // Reiniciar o cooldown
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, hitBoxSize);
    }
}
