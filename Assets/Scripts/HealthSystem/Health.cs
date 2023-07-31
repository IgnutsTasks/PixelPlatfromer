using System;
using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem
{
    public class Health : MonoBehaviour
    {
        [Header("General")] 
        [SerializeField] private int maxValue = 5;
        [SerializeField] private int startValue = 5;
    
        [Header("Events")]
        [SerializeField] private UnityEvent<int> onChanged;
        [SerializeField] private UnityEvent onZero;
    
        private int _value;

        public int MAXValue => maxValue;
        public int StartValue => startValue;

        private void Awake()
        {
            _value = startValue;
            
            OnAwake();
        }
        
        public virtual void OnAwake() {}

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
            
                onChanged?.Invoke(_value);

                if (_value <= 0)
                {
                    onZero?.Invoke();
                }
            }
        }

        public event UnityAction<int> OnChanged
        {
            add => onChanged.AddListener(value);
            remove => onChanged.RemoveListener(value);
        }
    
        public event UnityAction OnZero
        {
            add => onZero.AddListener(value);
            remove => onZero.RemoveListener(value);
        }
    }
}
