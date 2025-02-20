using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform hero;
    private Enemy_Skeleton enemy;
    private int moveDir;

    private bool flippedOnce;

    public SkeletonBattleState(Enemy3 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        hero = HeroManager.instance.hero.transform;

        if (hero.GetComponent<HeroStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);

        stateTimer = enemy.battleTime;
        flippedOnce = false;
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
            }
        }
        else
        {
            if (flippedOnce == false)
            {
                flippedOnce = true;
                enemy.Flip();
            }

            if (stateTimer < 0 || Vector2.Distance(hero.transform.position, enemy.transform.position) > 6)
                stateMachine.ChangeState(enemy.idleState);
        }

        float distanceToHeroX = Mathf.Abs(hero.position.x - enemy.transform.position.x);

        if(distanceToHeroX < .8f)
            return;

        if (hero.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (hero.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        // Ensure Counter Image is deactivated when exiting this state
        enemy.CloseCounterAttackWindow();
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
