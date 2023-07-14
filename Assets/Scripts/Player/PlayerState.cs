using System;
using UnityEngine;

namespace Player
{
    public abstract class PlayerState
    {
        public string Name { get; protected set; }
        
        public PlayerStateMachine PlayerStateMachine { get; private set; }
        public Animator Animator { get; private set; }
        
        public event Action OnEnter;
        public event Action OnExit;
        
        public virtual void Initialize(PlayerStateMachine playerStateMachine, Animator animator)
        {
            PlayerStateMachine = playerStateMachine;
            Animator = animator;
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