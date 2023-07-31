using System;
using System.Collections.Generic;
using HealthSystem;
using UnityEngine;

namespace Player
{
    public class PlayerHealthView : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private HealthView healthViewPrefab;
        [SerializeField] private Transform layout;

        private readonly List<HealthView> _createdHealthView = new List<HealthView>();

        private void Start()
        {
            Initialize();
            UpdateUI();

            PlayerHealth.Instance.OnChanged += val =>
            {
                UpdateUI();
            };
        }

        private void Initialize()
        {
            for (int i = 0; i < PlayerHealth.Instance.MAXValue; i++)
            {
                CreateHealthView();
            }
        }

        private void CreateHealthView()
        {
            HealthView newHealthView = Instantiate(healthViewPrefab, layout);
            newHealthView.SetActive(true);
            
            _createdHealthView.Add(newHealthView);
        }

        private void UpdateUI()
        {
            int stepsCount = PlayerHealth.Instance.MAXValue - PlayerHealth.Instance.Value;
            
            for (int i = _createdHealthView.Count - 1; i >= 0; i--)
            {
                if (stepsCount == 0) return;
                stepsCount--;

                if (_createdHealthView[i].IsActive == false) continue;
                
                _createdHealthView[i].SetActive(false);
            }
        }
    }
}