using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamagable
{

    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;
    protected Entity_Stats entityStats { get; private set; }

    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private Vector2 onDamageKnockback = new Vector2(1.5f, 2.5f);

    [Header("On Heavy Damage Knockback")]
    [Range(0, 1)]
    [SerializeField] private float heavyDamageThreshold = 0.3f;
    [SerializeField] private float heavyKnockbackDuration = 0.5f;
    [SerializeField] private Vector2 onHeavyDamageKnockback = new Vector2(7f, 7f);

    protected virtual void Awake()
    {

        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();
        entityStats = GetComponent<Entity_Stats>();

        currentHealth = entityStats.GetMaxHealth();
        isDead = false;
        UpdateHealthBar();
    }

    public virtual bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (isDead)
            return false;

        if (AttackEvaded())
        {
            Debug.Log($"{gameObject.name} evaded the attack!");
            return false;
        }

        Entity_Stats damageDealerStats = damageDealer.GetComponent<Entity_Stats>();
        float armorReduction = damageDealerStats != null ? damageDealerStats.GetArmorReduction() : 0f;

        float armorMitigation = entityStats.GetArmorMitigation(armorReduction);
        float physicalDamageTaken = damage * (1 - armorMitigation);

        float elementalResistance = entityStats.GetElementalResistance(element);
        float elementalDamageTaken = elementalDamage * (1 - elementalResistance);

        TakeKnockback(physicalDamageTaken, damageDealer);

        ReduceHealth(physicalDamageTaken + elementalDamageTaken);

        return true;
    }

    private void TakeKnockback(float damage, Transform damageDealer)
    {
        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateKnockbackDuration(damage);
        entity?.ReciveKnockback(knockback, duration);
    }

    private bool AttackEvaded() => Random.Range(0f, 100f) < entityStats.GetEvasion();

    public void ReduceHealth(float damage)
    {
        entityVfx?.PlayOnDamageVfx();
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;

        healthBar.value = currentHealth / entityStats.GetMaxHealth();
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? onHeavyDamageKnockback : onDamageKnockback;
        knockback.x *= direction;

        return knockback;
    }

    private float CalculateKnockbackDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

    private bool IsHeavyDamage(float damage) => damage / entityStats.GetMaxHealth() >= heavyDamageThreshold;
}
