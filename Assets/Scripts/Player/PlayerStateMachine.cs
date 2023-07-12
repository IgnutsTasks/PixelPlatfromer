using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private float groundCheckDistance;

        [Header("Wall sliding")]
        [SerializeField] private LayerMask whatIsWall;
        [SerializeField] private float speedSliding = -4;
        [SerializeField] private Transform wallCheckPoint;
        [SerializeField] private float wallCheckDistance;
        
        public int IsRunAnimation => Animator.StringToHash("IsRun");
        public int OnJumpAnimation => Animator.StringToHash("OnJump");
        public int IsGroundedAnimation => Animator.StringToHash("IsGrounded");
        public int IsFallAnimation => Animator.StringToHash("IsFall");
        public int OnSlideAnimation => Animator.StringToHash("OnSlide");
        public int IsSlideAnimation => Animator.StringToHash("IsSlide");
    
        public bool IsGrounded { get; private set; }    
        public bool IsWalled { get; private set; }
        
        public IdleState IdleState { get; private set; }
        public RunState RunState { get; private set; }
        public DeathState DeathState { get; private set; }
        
        public EntityState CurrentState { get; private set; }
        

        public PJumpState PJumpState { get; private set; }
        public PWallClimbing PWallClimbing { get; private set; }

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
            PWallClimbing = new PWallClimbing(speedSliding);
        }

        private void Start()
        {
            IdleState.Initialize(this, animator);
            RunState.Initialize(this, animator);
            DeathState.Initialize(this, animator);
            
            SetState(RunState);
            
            PJumpState.Initialize(this, animator);
            PWallClimbing.Initialize(this, animator);
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
            
            Array.ForEach(ParallelStates.ToArray(), state => state.FixedUpdate());

            IsGrounded = CheckGround();
            IsWalled = CheckWall();
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
            if (ParallelStates.Contains(parallelState)) return;
            
            ParallelStates.Add(parallelState);
            parallelState.Enter();
        }

        public void RemoveParallelState(ParallelState parallelState)
        {
            ParallelStates.Remove(parallelState);
            parallelState.Exit();
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
            
            return raycastHit && Vector3.Distance(raycastHit.point, groundCheckPoint.position) < groundCheckDistance;
        }

        private bool CheckWall()
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(
                wallCheckPoint.position, 
                Vector2.right,
                Mathf.Infinity,
                whatIsWall);
            
            return raycastHit && Vector3.Distance(raycastHit.point, wallCheckPoint.position) < wallCheckDistance;
        }
    }
}