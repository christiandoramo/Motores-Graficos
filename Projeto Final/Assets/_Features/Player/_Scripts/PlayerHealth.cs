using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth = 1;

    private float currentHealth;

    //private static int collisionCount = 0; // Contador de colisões
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        Debug.Log($"Dano no inimigo ({name}) recebido: {damage}, \nvida atual: {currentHealth}");

        if (currentHealth <= 0)
        {
            //Die();
        }
    }
}
