using UnityEngine;

namespace Player
{
    public class PlayerAttribute : MonoBehaviour
    {
        [Header("Static Attributes")] 
        public float maxHealth;
        public float maxStamina;
        public float maxCharge;
        public float staminaRecovery;
        public float attack;
        [Header("Dynamic Attributes")]
        public float health;
        public float stamina;
        public float charge;

        private void Update()
        {
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            health = Mathf.Clamp(health, 0, maxHealth);
            charge = Mathf.Clamp(charge, 0, maxCharge);
        }
    }
}
