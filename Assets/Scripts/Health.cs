using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> onChanged;
    [SerializeField] private UnityEvent onZero;
    
    private float _value;

    public float Value
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

    public event UnityAction<float> OnChanged
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
