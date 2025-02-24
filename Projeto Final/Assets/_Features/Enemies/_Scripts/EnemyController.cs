using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public bool isDying = false;
    public bool isAttacking = false;
    public bool isMoving = false;
    private EnemyMove enemyMove;
    void Start()
    {
        // Tenta pegar o Animator no próprio GameObject ou no pai
        if (!TryGetComponent(out enemyMove))
        {
            enemyMove = GetComponentInParent<EnemyMove>();
        }

        if (!TryGetComponent(out animator))
        {
            animator = GetComponentInChildren<Animator>();
        }
    }
    public void UpdateAnimatorParameters()
    {
        animator.SetBool("IsDying", isDying);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsAttacking", isAttacking);

        // if (velocity.magnitude > 0.1f)
        // {
        //     animator.SetFloat("moveX", velocity.x);
        //     animator.SetFloat("moveY", velocity.y);
        // }
    }
}
