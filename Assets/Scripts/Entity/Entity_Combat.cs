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
    [SerializeField] private float electrifyChargeBuildUp = 0.4f;
    [Space]
    [SerializeField] private float fireScale = 1;
    [SerializeField] private float lightningScale = 2.5f;

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

            float elementalDamage = stats.GetElementalDamage(out ElementType element, 0.6f);
            float damage = stats.GetPhysicalDamage(out bool isCrit);
            bool targetGotHit = damagable.TakeDamage(damage, elementalDamage, element, transform);

            if (element != ElementType.None)
                ApplyStatusEffect(target.transform, element);

            if (targetGotHit)
            {
                entityVfx.UpdateOnHitVfxColor(element);
                entityVfx.CreateOnHitVfx(target.transform, isCrit);
            }

        }
    }

    public void ApplyStatusEffect(Transform target, ElementType element, float scaleFactor = 1)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();
        if (statusHandler == null)
            return;

        if (element == ElementType.Ice && statusHandler.canBeApplied(ElementType.Ice))
            statusHandler.ApplyChillEffect(chilledSlowMultiplier, defaultDuration);

        if (element == ElementType.Fire && statusHandler.canBeApplied(ElementType.Fire))
        {
            scaleFactor = fireScale;
            float fireDamage = stats.offense.fireDamage.GetValue() * scaleFactor;
            statusHandler.ApplyBurnEffect(defaultDuration, fireDamage);
        }

        if (element == ElementType.Lightning && statusHandler.canBeApplied(ElementType.Lightning))
        {
            scaleFactor = lightningScale;
            float lightningDamage = stats.offense.lightningDamage.GetValue() * scaleFactor;
            statusHandler.ApplyElectrifyEffect(defaultDuration, lightningDamage, electrifyChargeBuildUp);
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
