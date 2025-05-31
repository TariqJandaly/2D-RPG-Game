using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected Player player;
    protected string animName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;
    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(Player player, StateMachine stateMachine, string animName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animName = animName;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }


    // Called every time the entity enter any state.
    public virtual void Enter()
    {
        anim.SetBool(animName, true);
        triggerCalled = false;
    }

    //  Called each frame when the state is active.
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);
    }

    // Called every time the entity exit any state.
    public virtual void Exit()
    {
        anim.SetBool(animName, false);
    }
    
    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    private bool CanDash()
    {
        if (player.wallDetected)
            return false;

        if (stateMachine.currentState == player.dashState)
            return false;

        return true;
    }
}