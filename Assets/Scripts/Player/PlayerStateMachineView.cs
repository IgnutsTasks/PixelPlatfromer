using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachineView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerInfoText;

        private string _outputText;
        
        private void Update()
        {
            UpdatePlayerInfo();
            
            DrayRays();
        }

        private void UpdatePlayerInfo()
        {
            _outputText = "PStates: \n";

            foreach (var pstate in PlayerStateMachine.Instance.ParallelStates)
            {
                _outputText += $"{pstate.Name}\n";
            }

            playerInfoText.text = _outputText;
        }
        
        private void DrayRays()
        {
            Color color = PlayerStateMachine.Instance.IsGrounded ? Color.green : Color.red;
            Debug.DrawRay(transform.position, transform.up * -1, color);
            
            color = PlayerStateMachine.Instance.IsWalled ? Color.green : Color.red;
            Debug.DrawRay(transform.position + Vector3.up * 0.6f, transform.right, color);
        }
    }
}