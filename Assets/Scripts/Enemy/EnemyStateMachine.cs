using System;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [Header("Graphic")]
        [SerializeField] private Animator animator;

        [Header("IdleToRun state")] 
        [SerializeField] private float timeToRun = 2f;
        
        [Header("Patrol state")]
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float patrolSpeed = 2;

        [Header("Shadow state")] 
        [SerializeField] private TriggerHandler shadowStateZone;
        [SerializeField] private TriggerHandler relaxZone;
        [SerializeField] private float shadowSpeed = 3;

        [Header("Attack state")] 
        [SerializeField] private TriggerHandler attackZone;
        [SerializeField] private float attackRate = 1;
        

        private Rigidbody2D _rigidbody;
        
        public int IsRunAnimation => Animator.StringToHash("IsRun");
        public int OnAttackAnimation => Animator.StringToHash("OnAttack");
        
        public IdleToRunState IdleToRunState { get; private set; }
        public PatrolState PatrolState { get; private set; }
        public ShadowState ShadowState { get; private set; }
        public AttackState AttackState { get; private set; }
        
        public EnemyState CurrentState { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            IdleToRunState = new IdleToRunState(timeToRun);
            PatrolState = new PatrolState(_rigidbody, patrolPoints, patrolSpeed);
            ShadowState = new ShadowState(_rigidbody, shadowSpeed, shadowStateZone, relaxZone);
            AttackState = new AttackState(attackRate, attackZone);
        }

        private void Start()
        {
            PatrolState.Initialize(this, animator);
            IdleToRunState.Initialize(this, animator);
            ShadowState.Initialize(this, animator);
            AttackState.Initialize(this, animator);
            
            SetState(PatrolState);
        }

        private void Update()
        {
            CurrentState.Update();
        }

        private void FixedUpdate()
        {
            CurrentState.FixedUpdate();
        }

        public void SetState(EnemyState enemyState)
        {
            CurrentState?.Exit();
            CurrentState = enemyState;
            CurrentState?.Enter();
        }
        
        public void LookAtDirection(Vector3 directionPoint)
        {
            Vector2 scale = transform.localScale;
            scale.x = directionPoint.x < transform.position.x ? Mathf.Abs(scale.x) : Mathf.Abs(scale.x) * -1;

            transform.localScale = scale;
        }
    }
}