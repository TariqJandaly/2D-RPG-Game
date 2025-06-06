using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;
    public Enemy_StunnedState stunnedState;

    [Header("Battle Details")]
    public float battleMoveSpeed = 3f;
    public float attackDistance = 2f;
    public float battleTimeDuration = 5f;
    public float minRetreatDistance = 1f;
    public Vector2 retreatVelocity;

    [Header("Stunned state details")]
    public float stunnedDuration = 1f;
    public Vector2 stunnedVelocity = new Vector2(7, 7);
    [SerializeField] protected bool canBeStunned;


    [Header("Movement details")]
    public float idleTime = 2f;
    public float moveSpeed = 1.4f;

    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1f;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10f;
    public Transform player { get; private set; }

    protected override IEnumerator SlowDownEntityCo(float slowMultiplier, float duration)
    {
        float originalMoveSpeed = moveSpeed;
        float originalBattleMoveSpeed = battleMoveSpeed;
        float originalAnimSpeed = anim.speed;

        float speedMultiplier = 1f - slowMultiplier;

        moveSpeed *= speedMultiplier;
        battleMoveSpeed *= speedMultiplier;
        anim.speed *= speedMultiplier;

        yield return new WaitForSeconds(duration);

        moveSpeed = originalMoveSpeed;
        battleMoveSpeed = originalBattleMoveSpeed;
        anim.speed = originalAnimSpeed;
    }

    public void SetCounterWindow(bool enable) => canBeStunned = enable;

    public override void EntityDeath()
    {
        base.EntityDeath();

        stateMachine.ChangeState(deadState);
    }

    public void TryEnterBattleState(Transform player)
    {

        if (
            stateMachine.currentState == battleState ||
            stateMachine.currentState == attackState ||
            stateMachine.currentState == deadState
        )
            return;

        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    private void HandlePlayerDeath()
    {
        stateMachine.ChangeState(idleState);
    }

    public Transform GetPlayerReference()
    {

        if (player == null)
            player = PlayerDetected().transform;

        return player;
    }

    public RaycastHit2D PlayerDetected()
    {

        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        float changeY = 0f;

        changeY = 0.1f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position + new Vector3(0, changeY, 0), new Vector3(playerCheck.position.x + facingDir * playerCheckDistance, playerCheck.position.y + changeY, 0));

        changeY = 0f;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position + new Vector3(0, changeY, 0), new Vector3(playerCheck.position.x + facingDir * attackDistance, playerCheck.position.y + changeY, 0));

        changeY = -0.1f;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position + new Vector3(0, changeY, 0), new Vector3(playerCheck.position.x + facingDir * minRetreatDistance, playerCheck.position.y + changeY, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)retreatVelocity);

    }

    void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDisable()
    {
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }

}
