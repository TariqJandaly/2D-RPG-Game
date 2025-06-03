using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected string animName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animName)
    {
        this.stateMachine = stateMachine;
        this.animName = animName;
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
        UpdateAnimationParameters();
    }

    // Called every time the entity exit any state.
    public virtual void Exit()
    {
        anim.SetBool(animName, false);
    }

    public void AnimationTrigger()
    {
        triggerCalled = true;
    }
    
    public virtual void UpdateAnimationParameters()
    {
        // This method can be overridden in derived classes to update specific animation parameters.
    }
}
