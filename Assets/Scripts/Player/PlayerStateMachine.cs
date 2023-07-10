using System;
using System.Collections.Generic;
using Player.ParallelStates;
using Player.States;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : Singletone<PlayerStateMachine>
    {
        [Header("Animator")] 
        [SerializeField] private Transform graphic;
        [SerializeField] private Animator animator;
        
        [Header("Movement properties")] 
        [SerializeField] private float runSpeed = 6f;

        [Header("Jump")] 
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float jumpForce = 450f;
        [SerializeField] private Transform groundCheckPoint; 
        [SerializeField] private float minGroundCheckDistance;
        
        public int IsRunAnimation => Animator.StringToHash("IsRun");
        public int OnJumpAnimation => Animator.StringToHash("OnJump");
        public int IsGroundedAnimation => Animator.StringToHash("IsGrounded");
        public int IsFallAnimation => Animator.StringToHash("IsFall");
    
        public bool IsGrounded { get; private set; }    
        
        public IdleState IdleState { get; private set; }
        public RunState RunState { get; private set; }
        public DeathState DeathState { get; private set; }
        
        public EntityState CurrentState { get; private set; }
        

        public PJumpState PJumpState { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }
        public List<ParallelState> ParallelStates { get; } = new List<ParallelState>();
        

        public override void OnAwake()
        {
            Instance = this;
            
            Rigidbody = GetComponent<Rigidbody2D>();
            
            IdleState = new IdleState();
            RunState = new RunState(Rigidbody, runSpeed);
            DeathState = new DeathState();
            
            PJumpState = new PJumpState(jumpForce);
        }

        private void Start()
        {
            IdleState.Initialize(this, animator);
            RunState.Initialize(this, animator);
            DeathState.Initialize(this, animator);
            
            SetState(RunState);
            
            PJumpState.Initialize(this, animator);
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
            
            Array.ForEach(ParallelStates.ToArray(), state => state.FixedUpdate());

            IsGrounded = CheckGround();
        }

        private void Update()
        {
            CurrentState?.Update();
            
            Array.ForEach(ParallelStates.ToArray(), state => state.Update());
            
            animator.SetBool(IsGroundedAnimation, IsGrounded);
            animator.SetBool(IsFallAnimation, Rigidbody.velocity.y < 0);
        }

        public void SetState(EntityState entityState)
        {
            if (entityState == CurrentState) return;
            if (CurrentState == DeathState) return;
            
            CurrentState?.Exit();
            CurrentState = entityState;
            CurrentState.Enter();
        }

        public void AddParallelState(ParallelState parallelState)
        {
            parallelState.Enter();
            ParallelStates.Add(parallelState);
        }

        public void RemoveParallelState(ParallelState parallelState)
        {
            parallelState.Exit();
            ParallelStates.Remove(parallelState);
        }

        public void LookAtDirection(float moveX)
        {
            Vector2 scale = transform.localScale;
            scale.x = moveX > 0 ? Mathf.Abs(scale.x) : Mathf.Abs(scale.x) * -1;

            transform.localScale = scale;
        }

        private bool CheckGround()
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(
                groundCheckPoint.position, 
                Vector2.down,
                Mathf.Infinity,
                whatIsGround);
            
            return raycastHit && Vector3.Distance(raycastHit.point, groundCheckPoint.position) < minGroundCheckDistance;
        }
    }
}