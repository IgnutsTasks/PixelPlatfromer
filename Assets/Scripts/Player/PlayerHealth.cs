using System;
using HealthSystem;

namespace Player
{
    public class PlayerHealth : Health
    {
        public static PlayerHealth Instance { get; private set; }

        public override void OnAwake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}