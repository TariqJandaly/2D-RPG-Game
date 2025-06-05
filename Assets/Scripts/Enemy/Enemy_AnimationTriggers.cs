using UnityEngine;

public class Enemy_AnimationTriggers : Entity_AnimationTriggers
{
    Enemy enemy;
    Enemy_VFX enemyVfx;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponentInParent<Enemy>();
        enemyVfx = GetComponentInParent<Enemy_VFX>();
    }

    private void EnableCounterWindow()
    {
        enemy.SetCounterWindow(true);
        enemyVfx.EnableAttackAlert(true);
    }

    private void DisbaleCounterWindow()
    {
        enemy.SetCounterWindow(false);
        enemyVfx.EnableAttackAlert(false);
    }
}