using System.Linq.Expressions;
using Common;
using UnityEngine;

namespace Player.ParallelStates
{
    public class PJumpState : ParallelState
    {
        private readonly float _jumpForce;

        public PJumpState(float jumpForce)
        {
            _jumpForce = jumpForce;
        }

        public override void Initialize(PlayerStateMachine playerStateMachine, Animator animator)
        {
            base.Initialize(playerStateMachine, animator);

            InputHandler.Instance.OnJump += () =>
            {
                if (PlayerStateMachine.IsGrounded == false) return;
                
                Animator.SetTrigger(PlayerStateMachine.OnJumpAnimation);
                PlayerStateMachine.AddParallelState(this);
            };
        }

        public override void Enter()
        {
            base.Enter();
            
            PlayerStateMachine.Rigidbody.AddForce(Vector2.up * _jumpForce);

            PlayerStateMachine.RemoveParallelState(this);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}