using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        if (!enemy.groundDetected || enemy.anyWallDetected)
            enemy.Flip();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);

        if (!enemy.groundDetected || enemy.anyWallDetected)
            stateMachine.ChangeState(enemy.idleState);
    }
}
