using UnityEngine;

public class Enemy_GroundedState : EnemyState
{
    public Enemy_GroundedState(Enemy enemy, StateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {

    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.battleState);
    }


}
