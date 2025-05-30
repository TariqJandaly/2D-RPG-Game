using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected Player player;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.stateName = stateName;
    }


    // Called every time the entity enter any state.
    public virtual void Enter()
    {
        Debug.Log($"Enter: {stateName}");    }
    
    //  Called each frame when the state is active.
    public virtual void Update()
    {
        Debug.Log($"Updating: {stateName}");
    }

    // Called every time the entity exit any state.
    public virtual void Exit()
    {
        Debug.Log($"Exit: {stateName}");
    }
}