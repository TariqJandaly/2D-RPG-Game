using UnityEngine;

public class Enemy_BattleState : EnemyState
{

    private Transform player;
    private float lastTimeWasInBattle = 0f;

    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
            player = enemy.PlayerDetected().transform;

        if (WithinRetreatRange())
        {
            enemy.rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());       
        }

    }

    public override void Update()
    {
        base.Update();
        

        if (enemy.PlayerDetected())
            lastTimeWasInBattle = Time.time;
            
        if (DirectionToPlayer() != enemy.facingDir)
            enemy.Flip();

        else if (BattleTimeIsOver())
            stateMachine.ChangeState(enemy.idleState);

        if (WithinAttackRange() && enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.attackState);

        else if (!WithinAttackRange() && !enemy.anyWallDetected)
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
            
            
    }

    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    private bool WithinRetreatRange() => DistanceToPlayer() < enemy.minRetreatDistance;

    private bool BattleTimeIsOver() => Time.time >= enemy.battleTimeDuration + lastTimeWasInBattle;

    private float DistanceToPlayer()
    {
        if (player == null)
            return float.MaxValue;

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {
        if (player == null)
            return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }


}
