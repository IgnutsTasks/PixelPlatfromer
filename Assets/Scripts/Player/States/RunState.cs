using Common;
using UnityEngine;

namespace Player.States
{
    public class RunState : PlayerState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speedMovement;

        private Vector2 _velocity;
        
        public RunState(Rigidbody2D rigidbody, float speedMovement)
        {
            _rigidbody = rigidbody;
            _speedMovement = speedMovement;
        }

        public override void Initialize(PlayerStateMachine playerStateMachine, Animator animator)
        {
            base.Initialize(playerStateMachine, animator);

            InputHandler.Instance.OnMove += value =>
            {
                if (value == 0) return;
                
                _velocity = new Vector2(value * _speedMovement, _rigidbody.velocity.y);
                PlayerStateMachine.SetState(this);
            };
        }

        public override void Enter()
        {
            base.Enter();
            Animator.SetBool(PlayerStateMachine.IsRunAnimation, true);
        }

        public override void Update()
        {
            PlayerStateMachine.LookAtDirection(_velocity.x);
        }

        public override void FixedUpdate()
        {
            _rigidbody.velocity = _velocity;
        }
    }
}