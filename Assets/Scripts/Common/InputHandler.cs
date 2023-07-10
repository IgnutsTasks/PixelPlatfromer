using System;
using UnityEngine;

namespace Common
{
    public class InputHandler : Singletone<InputHandler>
    {
        public event Action OnJump;
        public event Action<float> OnMove;
        
        public override void OnAwake()
        {
            Instance = this;
        }

        public void Update()
        {
            float moveX = Input.GetAxisRaw("Horizontal");

            OnMove?.Invoke(moveX);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJump?.Invoke();
            }
        }
    }
}