using System.Collections;
using System.Collections.Generic;
using Common;
using Enemy.States;
using UnityEngine;

namespace Player.ParallelStates
{
    public class PAttackState : ParallelState
    {
        private readonly float _attackDelay;
        private readonly List<int> _animations = new List<int>();


        private bool _canAttack = true;
        private int _currentAttackAnimationIndex;

        public PAttackState(float attackDelay)
        {
            _attackDelay = attackDelay;
        }

        public override void Initialize(PlayerStateMachine playerStateMachine, Animator animator)
        {
            base.Initialize(playerStateMachine, animator);

            InputHandler.Instance.OnAttack += () =>
            {
                if (_canAttack == false) return;
                
                Attack();
            };
            
            _animations.Add(PlayerStateMachine.OnFirstAttackAnimation);
            _animations.Add(PlayerStateMachine.OnSecondAttackAnimation);
            _animations.Add(PlayerStateMachine.OnThirdAttackAnimation);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }

        private void Attack()
        {
            Animator.SetTrigger(_animations[_currentAttackAnimationIndex++]);

            if (_currentAttackAnimationIndex >= _animations.Count)
            {
                _currentAttackAnimationIndex = 0;
            }
            
            PlayerStateMachine.StartCoroutine(AttackDelayCoroutine());
            PlayerStateMachine.RemoveParallelState(this);
        }

        private IEnumerator AttackDelayCoroutine()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_attackDelay);
            _canAttack = true;
        }
    }
}