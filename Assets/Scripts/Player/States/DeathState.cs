using UnityEngine;

namespace Player.States
{
    public class DeathState : PlayerState
    {
        public override void Initialize(PlayerStateMachine playerStateMachine, Animator animator)
        {
            base.Initialize(playerStateMachine, animator);

            PlayerHealth.Instance.OnZero += () =>
            {
                PlayerStateMachine.SetState(this);
            };
        }

        public override void Enter()
        {
            base.Enter();
            
            Animator.SetTrigger(PlayerStateMachine.OnDeath);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}