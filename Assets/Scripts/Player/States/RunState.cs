using Common;
using UnityEngine;

namespace Player.States
{
    public class RunState : EntityState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speedMovement;

        private Vector2 _velocity;
        
        public RunState(Rigidbody2D rigidbody, float speedMovement)
        {
            _rigidbody = rigidbody;
            _speedMovement = speedMovement;
        }

        public override void Initialize(PlayerStateMachine playerStateMachine)
        {
            base.Initialize(playerStateMachine);

            InputHandler.Instance.OnMove += value =>
            {
                if (value == 0) return;
                
                _velocity = new Vector2(value * _speedMovement, _rigidbody.velocity.y);
                PlayerStateMachine.SetState(this);
            };
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