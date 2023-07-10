using System;

namespace Player
{
    public abstract class EntityState
    {
        public PlayerStateMachine PlayerStateMachine { get; private set; }
        
        public event Action OnEnter;
        public event Action OnExit;
        
        public virtual void Initialize(PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;
        }

        public virtual void Enter()
        {
            OnEnter?.Invoke();
        }

        public virtual void Exit()
        {
            OnExit?.Invoke();
        }
        
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}