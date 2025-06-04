using UnityEngine;

public class Entity_Health : MonoBehaviour
{

    private Entity_VFX entityVfx;
    private Entity entity;

    [SerializeField] protected float maxHealth = 100f;
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

        currentHealth = maxHealth;
        isDead = false;
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateKnockbackDuration(damage);

        entityVfx?.PlayerOnDamageVfx();
        entity?.ReciveKnockback(knockback, duration);
        ReduceHealth(damage);
    }

    protected void ReduceHealth(float damage)
    {
        if (currentHealth - damage <= 0)
            Die();
        else
            currentHealth -= damage;
    }

    protected virtual void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? onHeavyDamageKnockback : onDamageKnockback;
        knockback.x *= direction;

        return knockback;
    }

    private float CalculateKnockbackDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

    private bool IsHeavyDamage(float damage) => damage / maxHealth >= heavyDamageThreshold;
}
