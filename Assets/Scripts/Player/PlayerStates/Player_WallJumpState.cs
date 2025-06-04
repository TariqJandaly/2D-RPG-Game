using UnityEngine;

public class Player_WallJumpState : PlayerState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0 && stateMachine.currentState != player.jumpAttackState)
            stateMachine.ChangeState(player.fallState);

        if (player.fullWallDetected)
            stateMachine.ChangeState(player.wallSlideState);

        if(input.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.jumpAttackState);

    }
}
