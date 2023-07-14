using UnityEngine;

namespace Enemy.States
{
    public class PatrolState : EnemyState
    {
        private readonly Transform[] _patrolPoints;
        private readonly float _speedMovement;

        private int _currentPatrolPointIndex;
        private Rigidbody2D _rigidbody;

        public Transform CurrentPatrolPoint => _patrolPoints[_currentPatrolPointIndex];

        public PatrolState(Rigidbody2D rigidbody, Transform[] patrolPoints, float speedMovement)
        {
            _rigidbody = rigidbody;
            _patrolPoints = patrolPoints;
            _speedMovement = speedMovement;
        }

        public override void Enter()
        {
            base.Enter();

            _currentPatrolPointIndex = Random.Range(0, _patrolPoints.Length);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            Move();
            CheckDestination();
        }

        private void Move()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position,
                CurrentPatrolPoint.position,
                Time.deltaTime * _speedMovement);
            
            _rigidbody.MovePosition(newPosition);
        }

        private void CheckDestination()
        {
            if ((Vector3) _rigidbody.position != CurrentPatrolPoint.position) return;
            
            // logic
        }
    }
}