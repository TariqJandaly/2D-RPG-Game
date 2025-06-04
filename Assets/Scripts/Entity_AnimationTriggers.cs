using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{

    Entity entity;
    Entity_Combat entityCombat;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<Entity_Combat>();
    }

    private void CurrentStateTrigger()
    {
        // Get access to the Player component
        entity.CurrentStateAnimationTrigger();
    }

    private void AttackTrigger()
    {
        entityCombat.PerformAttack();
    }

}
