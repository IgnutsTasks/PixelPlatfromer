using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.States
{
    public class PatrolState : EnemyState
    {
        private readonly Transform[] _patrolPoints;
        private readonly float _speedMovement;

        private int _currentPatrolPointIndex;
        private Rigidbody2D _rigidbody;

        public Transform CurrentPatrolPoint => _patrolPoints[_currentPatrolPointIndex];

        public event Action OnNewTarget; 

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
            
            Animator.SetBool(EnemyStateMachine.IsRunAnimation, true);
        }

        public override void Update()
        {
            EnemyStateMachine.LookAtDirection(CurrentPatrolPoint.position);
            
            CheckPoints();
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
            if (Math.Round(_rigidbody.position.x, 2) != Math.Round(CurrentPatrolPoint.position.x, 2)) return;

            var lastIndex = _currentPatrolPointIndex;
            _currentPatrolPointIndex = Random.Range(0, _patrolPoints.Length);
            
            if (_currentPatrolPointIndex == lastIndex)
            {
                _currentPatrolPointIndex += 1;
            }
            
            OnNewTarget?.Invoke();
        }

        private void CheckPoints()
        {
            foreach (var point in _patrolPoints)
            {
                point.position = new Vector3(point.position.x,  EnemyStateMachine.transform.position.y);
            }
        }
    }
}