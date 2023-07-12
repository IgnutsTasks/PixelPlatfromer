using Common;
using UnityEngine;

namespace Player.ParallelStates
{
    public class PWallClimbing : ParallelState
    {
        private readonly float _speedSliding;

        public PWallClimbing(float speedSliding)
        {
            Name = "PWall";
            
            _speedSliding = speedSliding;
        }

        public override void Initialize(PlayerStateMachine playerStateMachine, Animator animator)
        {
            base.Initialize(playerStateMachine, animator);

            InputHandler.Instance.OnMove += value =>
            {
                if (PlayerStateMachine.IsWalled == false || PlayerStateMachine.IsGrounded) return;
                if (PlayerStateMachine.Rigidbody.velocity.y >= 0) return;
                
                PlayerStateMachine.AddParallelState(this);
            };

            InputHandler.Instance.OnJump += () =>
            {
                if (PlayerStateMachine.IsWalled == false) return;
                
                PlayerStateMachine.RemoveParallelState(this);

                PlayerStateMachine.Rigidbody.velocity = Vector2.zero;
                PlayerStateMachine.Rigidbody.AddForce(Vector2.up * 820);
            };
        }

        public override void Enter()
        {
            base.Enter();
            
            Animator.SetTrigger(PlayerStateMachine.OnSlideAnimation);
            Animator.SetBool(PlayerStateMachine.IsSlideAnimation, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            Animator.SetBool(PlayerStateMachine.IsSlideAnimation, false);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            if (PlayerStateMachine.IsWalled == false || PlayerStateMachine.IsGrounded)
            {
                PlayerStateMachine.RemoveParallelState(this);
                return;
            }
            
            PlayerStateMachine.Rigidbody.velocity = new Vector2(PlayerStateMachine.Rigidbody.velocity.x, _speedSliding);
        }
    }
}