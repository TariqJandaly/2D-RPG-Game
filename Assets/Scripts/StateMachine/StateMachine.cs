public class StateMachine
{
    public EntityState currentState { get; private set; }
    public bool canChangeState = true;

    public void Initialize(EntityState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        if (canChangeState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }

    public void UpdateActiveState()
    {
        currentState.Update();
    }

    public void SwitchOffStateMachine() => canChangeState = false;
}