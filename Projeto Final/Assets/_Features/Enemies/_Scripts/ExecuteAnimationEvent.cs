using UnityEngine;

public class ExecuteAnimationEvent : MonoBehaviour
{
    public void CallDie()
    {
        GetComponentInParent<EnemyHealth>().Die();
    }
}
