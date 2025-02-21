using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public GameObject attackPrefab;
    public Vector3 hitBoxSize = new(3, 3, 3);

    [Header("References")]
    [SerializeField] private Transform attackOriginPoint;
    PlayerMove playerMove;

    [SerializeField] private float coolDownTimer;
    [SerializeField] float torchCooldown;
        [SerializeField]
    LayerMask layerAttackMask;


    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0)) // Botão esquerdo do mouse para atacar
        {
            TorchAttack();
        }
    }

    public void TorchAttack()
    {
        if (playerMove.isGathering || playerMove.isCarrying || GameManager.instance.isDriving) return;

        if (coolDownTimer < torchCooldown) return;

        Vector3 direction = attackOriginPoint.forward;
        Vector3 attackArea = hitBoxSize;

        GameObject attackObj = Instantiate(attackPrefab, attackOriginPoint.position, Quaternion.identity);
        attackObj.GetComponent<TorchAttack>().Execute(direction, attackArea, layerAttackMask, Vector3.zero);

        coolDownTimer = 0f; // Reiniciar o cooldown
        Debug.Log("Player atacou");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, hitBoxSize);
    }
}