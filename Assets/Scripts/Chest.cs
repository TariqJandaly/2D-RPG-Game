using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{

    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();

    private bool isOpen;

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback = new Vector2(0, 3f);

    public void TakeDamage(float damage, Transform damageDealer)
    {
        if (isOpen)
            return;
        fx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200, 200);

        isOpen = true;

        // Drop items
    }
}
