using UnityEngine;

public class Player_BasicAttackState : PlayerState
{

    private float attackVelocityTimer;
    private float lastAttackTime;

    private bool comboAttackQueued = false;
    private int attackDir;
    private int comboIndex = 1;
    private int maxComboIndex = 3;
    private const int FirstComboIndex = 1;
    
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
        if (maxComboIndex != player.basicAttackVelocity.Length)
            Debug.LogWarning(
                $"Player_BasicAttackState: maxComboIndex ({maxComboIndex}) does not match the length of basicAttackVelocity array ({player.basicAttackVelocity.Length})."
            );
        maxComboIndex = player.basicAttackVelocity.Length;
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        lastAttackTime = Time.time;
        ResetComboIndexIfNeeded();

        // Define attack direction based on player input or facing direction
        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        // Detect and gamage enemies

        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextComboAttack();
        }

        if (triggerCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (
            comboIndex > maxComboIndex ||
            Time.time > player.comboResetTime + lastAttackTime
        )
            comboIndex = FirstComboIndex;
    }

    private void QueueNextComboAttack()
    {
        if (comboIndex < maxComboIndex)
        {
            comboAttackQueued = true;
        }
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.basicAttackVelocity[comboIndex - 1];

        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }
}
