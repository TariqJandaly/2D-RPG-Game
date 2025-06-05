using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    public float damage = 10f;
    public Entity_VFX entityVfx;


    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
    }

    public void PerformAttack()
    {
        foreach (Collider2D target in GetDetectedColliders())
        {

            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
                continue;

            damagable.TakeDamage(damage, transform);
            entityVfx.CreateOnHitVfx(target.transform.position);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.steelBlue;
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
