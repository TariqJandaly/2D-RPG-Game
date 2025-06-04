using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {

    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected)
            stateMachine.ChangeState(player.idleState);

        if (player.fullWallDetected)
            stateMachine.ChangeState(player.wallSlideState);
    }

}
