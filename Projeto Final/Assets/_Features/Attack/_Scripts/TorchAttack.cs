using System.Linq;
using UnityEngine;

public class TorchAttack : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float lifeTime;
    protected Rigidbody rb;
    protected float lifeTimer = 0;
    public Vector3 attackArea;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Execute(Vector3 direction, LayerMask layerMask, Vector3 projectileVelocity)
    {
        // Criar uma caixa colisora para o ataque corpo a corpo
        Collider[] hitColliders = Physics.OverlapBox(direction, attackArea * .5f, Quaternion.identity, layerMask);
        Collider collider = hitColliders.FirstOrDefault((collider) => collider != null);

        if (collider != null)
        {
            Debug.Log("Player acertou");
            if (collider.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Dano causado pelo player: " + damage);
            }
        }
        Debug.Log("Player atacou");

        Destroy(gameObject, lifeTime); // Destruir o objeto após o tempo de vida
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, attackArea);
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