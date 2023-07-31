using System.Collections;
using Common;
using Player;
using UnityEngine;

namespace Enemy.States
{
    public class AttackState : EnemyState
    {
        private readonly Transform _attackPoint;
        private readonly float _attackRadius;
        private readonly LayerMask _whatToAttack;

        private readonly TriggerHandler _attackZone;
        private readonly float _attackDelay;
        private readonly float _attackRate;

        private IEnumerator _attackCoroutine;

        public AttackState(float attackRate, TriggerHandler attackZone, Transform attackPoint, float attackRadius, 
            LayerMask whatToAttack, float attackDelay)
        {
            _attackRate = attackRate;
            _attackZone = attackZone;
            _attackDelay = attackDelay;

            _attackPoint = attackPoint;
            _attackRadius = attackRadius;
            _whatToAttack = whatToAttack;
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
            EnemyStateMachine.LookAtDirection(PlayerStateMachine.Instance.transform.position);
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

            EnemyStateMachine.StartCoroutine(Utils.AttackDelay(_attackDelay, () =>
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _whatToAttack);

                foreach (var col in colliders)
                {
                    if (col.isTrigger) continue;
                    if (col.gameObject == EnemyStateMachine.gameObject) continue;
                    if (col.TryGetComponent<PlayerHealth>(out var playerHealth) == false) continue;

                    playerHealth.Value -= 1;

                }
            }));
        }
    }
}