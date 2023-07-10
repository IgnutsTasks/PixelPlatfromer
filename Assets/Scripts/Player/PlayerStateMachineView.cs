using System;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachineView : MonoBehaviour
    {
        private void Update()
        {
            Color color = PlayerStateMachine.Instance.IsGrounded ? Color.green : Color.red;
            Debug.DrawRay(transform.position, transform.up * -1, color);
        }
    }
}