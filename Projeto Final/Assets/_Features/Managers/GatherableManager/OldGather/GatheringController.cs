
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatheringController : MonoBehaviour
{
    public PlayerController pc;
    public Transform arms;
    public LayerMask gatherableMask;
    private GameObject currentCarryablePrefab;
    public float weightGatheringMultiplier = 1f;
    private Coroutine coroutine = null;
    [SerializeField] private Vector3 boxSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(arms.transform.position, boxSize);
    }

    // criar classe separada para NPCs, Impostores fazer Gather
    public void Gather(InputAction.CallbackContext ctx)
    {
        if (pc.isGathering == true) return;

        if (pc.isCarrying == true)
        {
            DropCarryable(currentCarryablePrefab);
            return;
        }

        Collider[] colliders = Physics.OverlapBox(arms.position, boxSize * .5f, Quaternion.identity, gatherableMask);
        Collider collider = colliders.FirstOrDefault((collider) => collider != null);

        if (collider != null)
        {
            Debug.Log("Tentou Pegar");
            GameObject gatherable = collider.gameObject;
            if (pc.isCarrying == true && currentCarryablePrefab != null) DropCarryable(currentCarryablePrefab); // Apenas por seguran�a
            StartCarryGatherable(gatherable);
        }
        else
        {
            Debug.Log("Não achou");
        }
    }

    public void StartCarryGatherable(GameObject gatherable)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        pc.isGathering = true;
        pc.animator.SetBool("IsGathering", pc.isGathering);
        coroutine = StartCoroutine(CarryGatherable(gatherable));
    }
    private IEnumerator CarryGatherable(GameObject gatherable)
    {
        pc.animator.SetBool("IsGathering", pc.isGathering);
        float tempSpeed = pc.currentSpeed;
        pc.currentSpeed = 0;
        yield return new WaitForSeconds(.5f); // depois fazer evento de coleta com fim da animação
        pc.currentSpeed = tempSpeed;

        weightGatheringMultiplier = .95f;

        currentCarryablePrefab = CastGatherableObjToCarryableObj(gatherable);
        // currentCarryablePrefab.transform.SetParent(arms, false);

        pc.isCarrying = true;
        pc.isGathering = false;
        pc.animator.SetBool("IsGathering", pc.isGathering);
        pc.animator.SetBool("IsCarrying", pc.isCarrying);
    }
    // falta criar o drop - deve poder dropar automaticamente quando apertar F - pensar em logica novamente ao apertar F
    public void DropCarryable(GameObject carryable)
    {
        if (currentCarryablePrefab == null) return;
        weightGatheringMultiplier = 1f;
        currentCarryablePrefab.transform.SetParent(null, false);
        currentCarryablePrefab = null;

        weightGatheringMultiplier = 1f;

        currentCarryablePrefab = CastCarryableObjToGatherableObj(carryable);

        pc.isCarrying = false;
        pc.animator.SetBool("IsCarrying", pc.isCarrying);
    }

    private GameObject CastCarryableObjToGatherableObj(GameObject obj)
    {
        Carryable carryableScript = obj.GetComponent<Carryable>();
        GameObject gatherableObj = Instantiate(carryableScript.gatheringPrefab, obj.transform.position, Quaternion.identity);
        gatherableObj.GetComponent<Rigidbody>().AddForce(transform.forward * 5f + transform.up * 5f, ForceMode.Impulse);
        Destroy(obj);
        return null;
    }
    private GameObject CastGatherableObjToCarryableObj(GameObject obj)
    {
        Gatherable gatherableScript = obj.GetComponent<Gatherable>();
        obj.transform.position = Vector3.zero;

        Debug.Log("Arms Position Before: " + arms.position);
        GameObject carryableObj = Instantiate(gatherableScript.carryablePrefab, arms.position, arms.rotation * Quaternion.Euler(arms.rotation.x, arms.rotation.y, arms.rotation.z));
        Debug.Log("Arms Position After: " + arms.position);
        Destroy(obj);
        return carryableObj;
    }
    void Update()
    {
        // arms.SetPositionAndRotation(new Vector3(transform.position.x + transform.position.y + arms.transform.position.z), transform.rotation);
        if (currentCarryablePrefab != null)
        {
            currentCarryablePrefab.transform.SetPositionAndRotation(arms.position, arms.rotation); //* Quaternion.Euler(-90, 90, 0));
        }

    }
}
