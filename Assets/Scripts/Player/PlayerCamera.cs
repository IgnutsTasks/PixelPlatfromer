using System;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
        [SerializeField] private float speedMovement = 4f;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                PlayerStateMachine.Instance.transform.position + offset,
                speedMovement * Time.deltaTime);
        }
    }
}