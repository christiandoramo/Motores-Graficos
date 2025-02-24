using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private bool isDead; // blend tree buga animation event porque o float não é exatamente 1 ou 0 (logo as duas animações de mortes são executadas por debaixo dos panos - morrendo duas vezes),

    public float currentHealth;


    EnemyController ec;
    public float startingHealth = 1;

    //private static int collisionCount = 0; // Contador de colisões
    private void Start()
    {
        ec = GetComponent<EnemyController>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        if (ec.isDying == true) return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        Debug.Log($"Dano no inimigo ({name}) recebido: {damage}, \nvida atual: {currentHealth}");

        if (currentHealth <= 0)
        {
            SetDying();
        }
    }

    private void SetDying()
    {
        Debug.Log("Inimigo esta morrendo");
        ec.isDying = true;
        ec.UpdateAnimatorParameters();
        GetComponent<BoxCollider>().enabled = false;
    }

    public void Die()
    {
        if (isDead || this == null) return;
        Debug.Log("Inimigo " + name + " morreu");
        isDead = true;
        Destroy(gameObject);
    }
}
