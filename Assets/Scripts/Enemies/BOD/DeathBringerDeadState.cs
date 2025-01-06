using UnityEngine;

public class DeathBringerDeadState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerDeadState(Enemy3 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // Stop animation and disable the collider
        enemy.anim.SetBool(enemy.lastAnimBoolName, true);
        enemy.anim.speed = 0;
        enemy.cd.enabled = false;

        // Trigger the Victory menu
        VictoryMenuManager victoryMenu = GameObject.FindObjectOfType<VictoryMenuManager>();
        if (victoryMenu != null)
        {
            Debug.Log("VictoryMenu Found, Showing Menu");
            victoryMenu.ShowVictoryMenu();
        }
        else
        {
            Debug.LogError("VictoryMenu Not Found in Scene");
        }

        stateTimer = .15f;
    }

    public override void Update()
    {
        base.Update();

        // Maintain some vertical velocity during the death sequence
        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 10);
    }
}