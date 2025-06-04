using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    public float damageValue = 10f;


    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {
        foreach (Collider2D target in GetDetectedColliders())
        {
            Entity_Health targetHealth = target.GetComponent<Entity_Health>();
            targetHealth?.TakeDamage(damageValue, transform);
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.steelBlue;
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
