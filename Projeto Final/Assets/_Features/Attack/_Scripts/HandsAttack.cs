using UnityEngine;

public class HandsAttack : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float lifeTime;
    protected Rigidbody rb;
    protected float lifeTimer = 0;
    public Vector3 attackArea { get; private set; }

    public void Start()
    {
        damage = 1f; // Defina o dano específico para a tocha
        lifeTime = 0.25f; // Defina o tempo de vida específico para a tocha
        rb = GetComponent<Rigidbody>();
    }

    public void Execute(Vector3 direction, Vector3 attackArea, LayerMask layerMask, Vector3 projectileVelocity)
    {
        // Criar uma caixa colisora para o ataque corpo a corpo
        Collider[] hitColliders = Physics.OverlapBox(direction, attackArea *.5f, Quaternion.identity, layerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<PlayerHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Dano causado pelo inimigo: "+ damage+ "playerhealth: ");
            }
        }

        Destroy(gameObject, lifeTime); // Destruir o objeto após o tempo de vida
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * 1.5f, new Vector3(1, 1, 1));
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}