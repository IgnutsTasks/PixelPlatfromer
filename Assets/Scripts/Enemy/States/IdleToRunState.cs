using System.Collections;
using UnityEngine;

namespace Enemy.States
{
    public class IdleToRunState : EnemyState
    {
        private readonly float _timeToRun;

        private IEnumerator _startRunDelayCoroutine;
        

        public IdleToRunState(float timeToRun)
        {
            _timeToRun = timeToRun;
        }

        public override void Initialize(EnemyStateMachine enemyStateMachine, Animator animator)
        {
            base.Initialize(enemyStateMachine, animator);

            EnemyStateMachine.PatrolState.OnNewTarget += () =>
            {
                EnemyStateMachine.SetState(this);
            };
        }

        public override void Enter()
        {
            base.Enter();
            
            _startRunDelayCoroutine = StartRunDelay();
            EnemyStateMachine.StartCoroutine(_startRunDelayCoroutine);
        }

        public override void Exit()
        {
            base.Exit();
            
            EnemyStateMachine.StopCoroutine(_startRunDelayCoroutine);
        }

        public override void Update()
        {
            Animator.SetBool(EnemyStateMachine.IsRunAnimation, false);
        }

        public override void FixedUpdate()
        {
            
        }

        private IEnumerator StartRunDelay()
        {
            yield return new WaitForSeconds(_timeToRun);
            
            EnemyStateMachine.SetState(EnemyStateMachine.PatrolState);
        }
    }
}