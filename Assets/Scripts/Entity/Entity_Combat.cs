using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_Stats stats;
    public Entity_VFX entityVfx;


    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Status Effects Details")]
    [SerializeField] private float defaultDuration = 3f;
    [SerializeField] private float chilledSlowMultiplier = 0.2f;

    void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        foreach (Collider2D target in GetDetectedColliders())
        {

            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
                continue;

            float elementalDamage = stats.GetElementalDamage(out ElementType element);
            float damage = stats.GetPhysicalDamage(out bool isCrit);
            bool targetGotHit = damagable.TakeDamage(damage, elementalDamage, element, transform);

            if(element != ElementType.None)
                ApplyStatusEffect(target.transform, element);

            if (targetGotHit)
            {
                entityVfx.UpdateOnHitVfxColor(element);
                entityVfx.CreateOnHitVfx(target.transform, isCrit);
            }

        }
    }

    public void ApplyStatusEffect(Transform target, ElementType element)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();
        if (statusHandler == null)
            return;

        if (element == ElementType.Ice)
            statusHandler.ApplyChilledEffect(chilledSlowMultiplier, defaultDuration);

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
