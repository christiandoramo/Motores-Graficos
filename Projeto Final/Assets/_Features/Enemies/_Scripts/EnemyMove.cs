using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2f;
    public GameObject player;
    // [SerializeField] float stopDistance = .05f; // distancia da colisão

    public Rigidbody rb;
    [SerializeField] private LayerMask playerColliderMask;

    public Vector3 hitBoxSize = new(2, 2, 2);
    [SerializeField] private float coolDownTimer;
    [SerializeField] float torchCooldown;
    [SerializeField]
    LayerMask layerAttackMask;
    EnemyController ec;



    [Header("References")]
    [SerializeField] private Transform attackOriginPoint;
    [SerializeField] public GameObject attackPrefab;


    private void Start()
    {
        ec = GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject; // pega o player já na cena
    }
    private void Update()
    {
        if (ec.isDying)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer <= torchCooldown && ec.isAttacking == true)
        {
            ec.isAttacking = false;
        }

        FollowPlayer();
        if (coolDownTimer >= 10)
        {
            rb.linearVelocity = Vector3.zero;
            ec.isAttacking = false;
            ec.isMoving = true;


            coolDownTimer = 0;
        }
        ec.UpdateAnimatorParameters();

    }

    void FollowPlayer()
    {
        if (player == null) return;
        Collider[] colliders = Physics.OverlapBox(transform.position, hitBoxSize * .5f, Quaternion.identity, playerColliderMask);
        Collider collider = colliders.FirstOrDefault((collider) => collider != null);

        if (collider != null)
        {

            HandsAttack();

        }
        else if (ec.isAttacking == false)
        {
            Move();
        }

    }

    private void Move()
    {
        ec.isMoving = true;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        transform.rotation = Quaternion.LookRotation(direction); // Rotacionar na direção do movimento
    }

    private void HandsAttack()
    {
        ec.isMoving = false;
        ec.isAttacking = true;
        if (coolDownTimer < torchCooldown) return;

        Debug.Log("Inimigo gerou ataque");
        GameObject attackObj = Instantiate(attackPrefab, attackOriginPoint.position, Quaternion.identity);
        HandsAttack attack = attackObj.GetComponent<HandsAttack>();
        attack.Execute(attackObj.transform.position, layerAttackMask, Vector3.zero);

        coolDownTimer = 0f; // Reiniciar o cooldown
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, hitBoxSize);
    }
}
