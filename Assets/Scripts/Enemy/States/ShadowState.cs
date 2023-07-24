using Player;
using UnityEngine;

namespace Enemy.States
{
    public class ShadowState : EnemyState
    {
        private readonly float _speedMovement;
        private readonly TriggerHandler _angryZone;
        private readonly TriggerHandler _relaxZone;
        
        private Rigidbody2D _rigidbody;
        private Transform _player;

        public ShadowState(Rigidbody2D rigidbody, float speedMovement, TriggerHandler angryZone, TriggerHandler relaxZone)
        {
            _rigidbody = rigidbody;
            _speedMovement = speedMovement;
            _angryZone = angryZone;
            _relaxZone = relaxZone;
        }

        public override void Initialize(EnemyStateMachine enemyStateMachine, Animator animator)
        {
            base.Initialize(enemyStateMachine, animator);

            _angryZone.OnEnter += other =>
            {
                if (other.transform.TryGetComponent<PlayerStateMachine>(out var playerStateMachine) == false) return;

                _player = playerStateMachine.transform;
                EnemyStateMachine.SetState(this);
            };

            _relaxZone.OnExit += other =>
            {
                if (other.transform.TryGetComponent<PlayerStateMachine>(out var playerStateMachine) == false) return;

                EnemyStateMachine.SetState(EnemyStateMachine.IdleToRunState);
            };
        }

        public override void Enter()
        {
            base.Enter();
            
            Animator.SetBool(EnemyStateMachine.IsRunAnimation, true);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            Move();   
            EnemyStateMachine.LookAtDirection(_player.position);
        }

        private void Move()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position, 
                new Vector3(_player.position.x, _rigidbody.position.y), 
                _speedMovement * Time.deltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }
    }
}