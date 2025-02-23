using UnityEngine;

public class Gatherable : MonoBehaviour
{
    public bool isGatherable = true;
    public GameObject carryablePrefab;
    public Collectible collectibleType;
    private Rigidbody rb;

    [SerializeField] LayerMask wagonComponentMask;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb == null) return;
        if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Floor") || other.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SetAsChildAndDisablePhysics(other.transform);
        }
        else if (other.collider.CompareTag("Wagon"))
        {
            SetAsChildAndDisablePhysics(other.transform);
        }
        else if (other.collider.gameObject.layer == LayerMask.NameToLayer("WagonComponent"))
        {
            SetAsChildAndDisablePhysics(other.transform);
        }
    }

    private void SetAsChildAndDisablePhysics(Transform parent)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.detectCollisions = false;
        Destroy(rb);
        rb = null;
        transform.SetParent(parent);
    }
}
