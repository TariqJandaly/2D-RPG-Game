using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == player.facingDir && player.fullWallDetected)
            return;

        if (player.moveInput.x != 0)
            stateMachine.ChangeState(player.moveState);


    }

}
