using Player.States;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [Header("Animator")] 
        [SerializeField] private Transform graphic;
        [SerializeField] private Animator animator;
        
        [Header("Movement properties")] 
        [SerializeField] private float runSpeed = 6f;

        private int IsRunAnimation => Animator.StringToHash("IsRun");
        private int OnDeath => Animator.StringToHash("OnDeath");


        public IdleState IdleState { get; private set; }
        public RunState RunState { get; private set; }

        public DeathState DeathState { get; private set; }
        
        public EntityState CurrentState { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            
            IdleState = new IdleState();
            RunState = new RunState(Rigidbody, runSpeed);
            DeathState = new DeathState();
            
            IdleState.Initialize(this);
            RunState.Initialize(this);
            DeathState.Initialize(this);

            RunState.OnEnter += () => animator.SetBool(IsRunAnimation, true);
            IdleState.OnEnter += () => animator.SetBool(IsRunAnimation, false);

            DeathState.OnEnter += () =>
            {
                animator.SetTrigger(OnDeath);
            };

            SetState(RunState);
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        private void Update()
        {
            CurrentState?.Update();
        }

        public void SetState(EntityState entityState)
        {
            if (entityState == CurrentState) return;
            if (CurrentState == DeathState) return;
            
            CurrentState?.Exit();
            CurrentState = entityState;
            CurrentState.Enter();
        }

        public void LookAtDirection(float moveX)
        {
            Vector2 scale = transform.localScale;
            scale.x = moveX > 0 ? Mathf.Abs(scale.x) : Mathf.Abs(scale.x) * -1;

            transform.localScale = scale;
        }
    }
}