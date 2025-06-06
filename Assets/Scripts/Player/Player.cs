using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{

    public static event Action OnPlayerDeath;

    public PlayerInputSet input { get; private set; }

    [Header("States")]
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }
    public Player_DeadState deadState { get; private set; }
    public Player_CounterAttackState counterAttackState { get; private set; }

    [Header("Attack Details")]
    public Vector2 jumpAttackVelocity;
    public Vector2[] basicAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1f;
    public Coroutine queuedAttackCo;


    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5f;
    public Vector2 wallJumpForce;
    [Range(0, 1)] public float inAirMoveMultiplier = 0.7f;
    [Range(0, 1)] public float wallSlideSlowMultiplier = 0.7f;
    public float dashDuration = 0.25f;
    public float dashSpeed = 20f;
    public Vector2 moveInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
        deadState = new Player_DeadState(this, stateMachine, "dead");
        counterAttackState = new Player_CounterAttackState(this, stateMachine, "counterAttack");
    }


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override IEnumerator SlowDownEntityCo(float slowMultiplier, float duration)
    {
        
        float originalMoveSpeed = moveSpeed;
        float originalJumpForce = jumpForce;
        float originalAnimSpeed = anim.speed;
        Vector2 originalWallJump = wallJumpForce;
        Vector2 originalJumpAttack = jumpAttackVelocity;
        Vector2[] originalAttackVelocity = basicAttackVelocity;

        float speedMultiplier = 1f - slowMultiplier;

        moveSpeed *= speedMultiplier;
        jumpForce *= speedMultiplier;
        anim.speed *= speedMultiplier;
        wallJumpForce *= speedMultiplier;
        jumpAttackVelocity *= speedMultiplier;

        for (int i = 0; i < basicAttackVelocity.Length; i++)
        {
            basicAttackVelocity[i] *= speedMultiplier;
        }

        yield return new WaitForSeconds(duration);

        moveSpeed = originalMoveSpeed;
        jumpForce = originalJumpForce;
        anim.speed = originalAnimSpeed;
        wallJumpForce = originalWallJump;
        jumpAttackVelocity = originalJumpAttack;

        for (int i = 0; i < basicAttackVelocity.Length; i++)
        {
            basicAttackVelocity[i] = originalAttackVelocity[i];
        }

    }

    public override void EntityDeath()
    {
        base.EntityDeath();

        OnPlayerDeath?.Invoke();

        stateMachine.ChangeState(deadState);
    }


    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCo != null)
            StopCoroutine(queuedAttackCo);

        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
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
}