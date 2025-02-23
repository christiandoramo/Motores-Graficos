using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;

    public float currentHealth;

    //private static int collisionCount = 0; // Contador de colisões
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        Debug.Log($"Dano no inimigo ({name}) recebido: {damage}, \nvida atual: {currentHealth}");
        GameManager.instance.hudManager.UpdateHPCount(currentHealth, (int)startingHealth);
        if (currentHealth <= 0)
        {
            //Die();
        }
    }
}
