using Common;
using HealthSystem;
using UnityEngine;

namespace Enemy.States
{
    public class DeathState : EnemyState
    {
        private readonly Health _enemyHealth;

        public DeathState(Health health)
        {
            _enemyHealth = health;
        }

        public override void Initialize(EnemyStateMachine enemyStateMachine, Animator animator)
        {
            base.Initialize(enemyStateMachine, animator);

            _enemyHealth.OnZero += () =>
            {
                EnemyStateMachine.SetState(EnemyStateMachine.DeathState);
            };

            InputHandler.Instance.OnAttack += () =>
            {
                EnemyStateMachine.SetState(EnemyStateMachine.DeathState);
            };
        }

        public override void Enter()
        {
            base.Enter();
            
            Animator.SetTrigger(EnemyStateMachine.OnDeathAnimation);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}