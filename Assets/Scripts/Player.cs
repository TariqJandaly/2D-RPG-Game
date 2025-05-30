using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;
    public Player_IdleState idleState { get; private set; }
    public PlayerInputSet input { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Vector2 moveInput { get; private set; }

    void Awake()
    {
        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "Idle State");
        moveState = new Player_MoveState(this, stateMachine, "Move State");

    }

    void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Movement.canceled += context => moveInput = Vector2.zero;
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.UpdateActiveState();
    }
}