using UnityEngine;

public class Player_CounterAttackState : PlayerState
{

    private Player_Combat combat;
    private bool counteredSomeone;

    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
        combat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = combat.GetCounterRecoveryDuration();
        counteredSomeone = combat.CounterAttackPerformed();

        anim.SetBool("counterAttackPerformed", counteredSomeone);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, 0);

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);

        if (stateTimer < 0 && !counteredSomeone)
            stateMachine.ChangeState(player.idleState);
    }
}
