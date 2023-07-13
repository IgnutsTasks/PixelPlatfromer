using Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player.ParallelStates
{
    public class PWallJumpState : ParallelState
    {
        private readonly float _jumpForce;

        public PWallJumpState(float jumpForce)
        {
            Name = "PWallJump";
            
            _jumpForce = jumpForce;
        }

        public override void Initialize(PlayerStateMachine playerStateMachine, Animator animator)
        {
            base.Initialize(playerStateMachine, animator);

            InputHandler.Instance.OnJump += () =>
            {
                if (PlayerStateMachine.HasParallelState(PlayerStateMachine.PWallClimbing) == false) return;
                
                PlayerStateMachine.RemoveParallelState(PlayerStateMachine.PWallClimbing);
                
                Animator.SetTrigger(PlayerStateMachine.OnJumpAnimation);
                PlayerStateMachine.AddParallelState(this);
            };
        }

        public override void Enter()
        {
            base.Enter();
            
            PlayerStateMachine.Rigidbody.velocity = Vector2.zero;
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