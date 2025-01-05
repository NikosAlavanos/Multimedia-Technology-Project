using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerBattleState : EnemyState
{
    private Enemy_DeathBringer enemy;
    private Transform hero;
    private int moveDir;

    public DeathBringerBattleState(Enemy3 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        hero = HeroManager.instance.hero.transform;

        //if (hero.GetComponent<HeroStats>().isDead)
        //stateMachine.ChangeState(enemy.moveState);


    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsHeroDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsHeroDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
                else
                    stateMachine.ChangeState(enemy.idleState);
            }
        }

        if (hero.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (hero.position.x < enemy.transform.position.x)
            moveDir = -1;

        if (enemy.IsHeroDetected() && enemy.IsHeroDetected().distance < enemy.attackDistance - .1f)
            return;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
