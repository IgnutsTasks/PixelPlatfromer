
using Common;
using UnityEngine;

namespace Player.States
{
    public class IdleState : EntityState
    {
        public override void Initialize(PlayerStateMachine playerStateMachine, Animator animator)
        {
            base.Initialize(playerStateMachine, animator);

            InputHandler.Instance.OnMove += value =>
            {
                if (value != 0) return;
                
                PlayerStateMachine.SetState(this);
                PlayerStateMachine.Rigidbody.velocity = new Vector2(0, PlayerStateMachine.Rigidbody.velocity.y);
            };
        }

        public override void Enter()
        {
            base.Enter();
            Animator.SetBool(PlayerStateMachine.IsRunAnimation, false);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}