using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyState
    {
        public string Name { get; protected set; }
        
        public EnemyStateMachine EnemyStateMachine { get; private set; }
        public Animator Animator { get; private set; }
        
        public event Action OnEnter;
        public event Action OnExit;
        
        public virtual void Initialize(EnemyStateMachine enemyStateMachine, Animator animator)
        {
            EnemyStateMachine = enemyStateMachine;
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