using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Common
{
    public class TriggerHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider2D> onEnter;
        [SerializeField] private UnityEvent<Collider2D> onExit;
        [SerializeField] private GameObject[] ignoreObjects;

        public event UnityAction<Collider2D> OnEnter
        {
            add => onEnter.AddListener(value);
            remove => onEnter.RemoveListener(value);
        }
        
        public event UnityAction<Collider2D> OnExit
        {
            add => onExit.AddListener(value);
            remove => onExit.RemoveListener(value);
        } 

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Array.Find(ignoreObjects, obj => obj == other.gameObject)) return;
            
            onEnter?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (Array.Find(ignoreObjects, obj => obj == other.gameObject)) return;
            
            onExit?.Invoke(other);
        }
    }
}
