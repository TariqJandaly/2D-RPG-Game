using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;
    private Entity_Stats stats;
    private ElementType currentEffect = ElementType.None;
    private Coroutine chilledEffectCo;

    private void Awake()
    {
        stats = GetComponent<Entity_Stats>();
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
    }

    public void ApplyChilledEffect(float slowMultiplier, float duration)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float reducedDuration = duration * (1 - iceResistance);


        if (chilledEffectCo != null)
            chilledEffectCo = null;

        chilledEffectCo = StartCoroutine(ChilledEffectCo(reducedDuration, slowMultiplier));
    }

    private IEnumerator ChilledEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(slowMultiplier, duration);
        currentEffect = ElementType.Ice;

        entityVfx?.PlayOnStatusVfx(duration, currentEffect);

        yield return new WaitForSeconds(duration);
        currentEffect = ElementType.None;
    }

    public bool canBeApplied(ElementType element)
    {
        // Check if the current element is None or if the new element is different
        return currentEffect != element;
    }
}
