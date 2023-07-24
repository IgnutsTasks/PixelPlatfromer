using System.Collections;
using Player;
using UnityEngine;

namespace Enemy.States
{
    public class AttackState : EnemyState
    {
        private readonly TriggerHandler _attackZone;
        private readonly float _attackRate;

        private IEnumerator _attackCoroutine;

        public AttackState(float attackRate, TriggerHandler attackZone)
        {
            _attackRate = attackRate;
            _attackZone = attackZone;
        }

        public override void Initialize(EnemyStateMachine enemyStateMachine, Animator animator)
        {
            base.Initialize(enemyStateMachine, animator);
            
            _attackZone.OnEnter += other => 
            {
                if (other.transform.TryGetComponent<PlayerStateMachine>(out var playerStateMachine) == false) return;
                
                EnemyStateMachine.SetState(this);
            };
        }

        public override void Enter()
        {
            base.Enter();

            Animator.SetBool(EnemyStateMachine.IsRunAnimation, false);
            
            _attackCoroutine = AttackCoroutine();
            EnemyStateMachine.StartCoroutine(_attackCoroutine);
        }

        public override void Exit()
        {
            base.Exit();
            
            EnemyStateMachine.StopCoroutine(_attackCoroutine);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }

        private IEnumerator AttackCoroutine()
        {
            while (true)
            {
                Attack();
                yield return new WaitForSeconds(_attackRate);
            }
        }

        private void Attack()
        {
            Animator.SetTrigger(EnemyStateMachine.OnAttackAnimation);
        }
    }
}