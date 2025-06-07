using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{

    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = 0.15f;
    private Material originalMaterial;

    [Header("On Doing Damage VFX")]
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject onDamageVfxPrefab;
    [SerializeField] private GameObject onCritDamageVfxPrefab;

    [Header("Element Colors")]
    [SerializeField] private Color burnVfx = Color.red;
    [SerializeField] private Color chillVfx = Color.cyan;
    [SerializeField] private Color electrifyVfx = Color.yellow;
    
    private Color originalHitVfxColor = Color.white;

    private Coroutine onDamageVfxCoroutine;
    private Coroutine playOnStatusVfx;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
        originalHitVfxColor = hitVfxColor;

        entity = GetComponent<Entity>();
    }

    public void PlayOnStatusVfx(float duration, ElementType element)
    {
        if (element == ElementType.Ice)
            StartCoroutine(PlayStatusVfxCo(duration, chillVfx));

        if (element == ElementType.Fire)
            StartCoroutine(PlayStatusVfxCo(duration, burnVfx));
        
        if (element == ElementType.Lightning)
            StartCoroutine(PlayStatusVfxCo(duration, electrifyVfx));
    }

    public void StopAllVfx()
    {
        StopAllCoroutines();
        sr.color = Color.white;
        sr.material = originalMaterial;

    }

    private IEnumerator PlayStatusVfxCo(float duration, Color effectColor)
    {
        float tickInterval = 0.25f;
        float timeHasPassed = 0f;

        Color lightColor = effectColor * 1.2f; // Slightly brighter color for the effect
        Color darkColor = effectColor * 0.9f; // Slightly darker color for the effect

        bool toggle = false;

        while (timeHasPassed < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(tickInterval);
            timeHasPassed += tickInterval;
        }

        sr.color = Color.white;
    }

    public void CreateOnHitVfx(Transform transform, bool isCrit)
    {
        GameObject hitVfxPrefab = isCrit ? onCritDamageVfxPrefab : onDamageVfxPrefab;
        GameObject vfx = Instantiate(hitVfxPrefab, transform.position, Quaternion.identity);

        SpriteRenderer vfxSr = vfx.GetComponentInChildren<SpriteRenderer>();
        vfxSr.color = hitVfxColor;

        if (entity.facingDir == -1 && isCrit)
            vfx.transform.Rotate(0f, 180f, 0f);

    }

    public void UpdateOnHitVfxColor(ElementType element)
    {
        if (element == ElementType.Fire)
            hitVfxColor = burnVfx;

        if (element == ElementType.Ice)
            hitVfxColor = chillVfx;

        if (element == ElementType.Lightning)
            hitVfxColor = electrifyVfx;

        if (element == ElementType.None)
            hitVfxColor = originalHitVfxColor;
    }

    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine);

        onDamageVfxCoroutine = StartCoroutine(onDamageVfxCo());
    }

    private IEnumerator onDamageVfxCo()
    {
        sr.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }

}
