using UnityEngine;

public class Player_DeadState : PlayerState
{
    public Player_DeadState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        input.Disable();

        rb.simulated = false;
    }
}
