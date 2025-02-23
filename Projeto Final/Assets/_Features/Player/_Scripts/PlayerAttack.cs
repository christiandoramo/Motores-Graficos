using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public GameObject attackPrefab;

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

        GameObject attackObj = Instantiate(attackPrefab, attackOriginPoint.position, Quaternion.identity);
        TorchAttack attack = attackObj.GetComponent<TorchAttack>();
        attack.Execute(attackObj.transform.position, layerAttackMask, Vector3.zero);


        coolDownTimer = 0f; // Reiniciar o cooldown
    }
}