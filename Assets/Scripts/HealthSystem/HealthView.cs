using System;
using UnityEngine;
using UnityEngine.UI;

namespace HealthSystem
{
    public class HealthView : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;

        private Image _avatarImage;
        
        public bool IsActive { get; private set; }

        private void Awake()
        {
            _avatarImage = GetComponent<Image>();
        }
        
        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            
            if (isActive)
            {
                _avatarImage.sprite = activeSprite;
                return;
            }

            _avatarImage.sprite = inactiveSprite;
        }
    }
}