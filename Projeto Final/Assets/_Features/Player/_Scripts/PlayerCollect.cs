using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollect : MonoBehaviour
{
    public PlayerMove pm;
    public LayerMask collectibleMask;
    [SerializeField] private Vector3 boxSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    // criar classe separada para NPCs, Impostores fazer Gather
    public void Collect()
    {

        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize * .5f, Quaternion.identity, collectibleMask);
        Collider collider = colliders.FirstOrDefault((collider) => collider != null);

        if (collider != null)
        {
            Debug.Log("Tentou coletar");
            GameObject collectible = collider.gameObject;
            StartCollect(collectible);
        }
        else
        {
            Debug.Log("Não achou");
        }
    }

    private void StartCollect(GameObject collectible)
    {
        // trocar por codigo de causar dano ao hitbox bater, quando HP do collectible chegar a 0, spawna o coletável (caso água ou óleo, basta apertar F mesmo e rodar animação de gatherable)

        ProceduralCollectible collectibleScript = collectible.GetComponent<ProceduralCollectible>();
        GameObject gatherableObj = Instantiate(collectibleScript.gatherablePrefab, collectible.transform.position + Vector3.up * 3f, Quaternion.identity);
        Destroy(collectible);
    }
}
