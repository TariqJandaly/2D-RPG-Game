using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {

    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);

        if (player.moveInput.x == 0 || player.fullWallDetected && player.moveInput.x == player.facingDir)
            stateMachine.ChangeState(player.idleState);


        
    }
}
