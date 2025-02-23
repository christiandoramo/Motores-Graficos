using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingEnemyHealth = 1;
    private bool isDead; // blend tree buga animation event porque o float não é exatamente 1 ou 0 (logo as duas animações de mortes são executadas por debaixo dos panos - morrendo duas vezes),

    private float currentHealth;

    EnemyController em;

    //private static int collisionCount = 0; // Contador de colisões
    private void Start()
    {
        em = GetComponent<EnemyController>();
        currentHealth = startingEnemyHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingEnemyHealth);
        Debug.Log($"Dano no inimigo ({name}) recebido: {damage}, \nvida atual: {currentHealth}");

        if (currentHealth <= 0)
        {
            SetDying();
        }
    }

    private void SetDying()
    {
        Debug.Log("Inimigo esta morrendo");
        em.isDying = true;
        em.animator.SetBool("IsDying", em.isDying);
        GetComponent<BoxCollider>().enabled = false;
    }

    public void Die()
    {
        if (isDead || this == null) return;
        Debug.Log("Inimigo " + name + " morreu");
        isDead = true;
        Destroy(this.gameObject);
    }
}
