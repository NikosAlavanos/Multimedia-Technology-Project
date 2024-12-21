using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Enemy_Skeleton enemy;
    protected Transform hero;

    public SkeletonGroundedState(Enemy3 _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        hero = HeroManager.instance.hero.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsHeroDetected() || Vector2.Distance(enemy.transform.position, hero.transform.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }
}
