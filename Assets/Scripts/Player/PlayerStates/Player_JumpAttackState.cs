using UnityEngine;

public class Player_JumpAttackState : PlayerState
{

    private bool touchedGround = false;

    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        touchedGround = false;

        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected && !touchedGround)
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocity.y);
        }

        if (triggerCalled && player.groundDetected)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
