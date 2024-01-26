using UnityEngine;

namespace Player
{
    public class SnowballAttack : MonoBehaviour
    {
        [Header("SnowballAttack Component Settings")]
        public GameObject snowballPrefab;
        public Transform startPosition;
        public LineRenderer aimingLineRenderer;
        public int lineSegmentCount = 20;
        [Header("SnowballAttack Parameters")]
        public float force;
        public float stamina;

        protected GameObject SnowballInstance;
        protected PlayerAttribute PlayerAttr;
        
        private Rigidbody _snowballRb;

        private void Awake()
        {
            PlayerAttr = GetComponent<PlayerAttribute>();    
        }

        public virtual void CreateSnowball()
        {
            SnowballInstance = Instantiate(snowballPrefab, startPosition.position, startPosition.rotation);
            _snowballRb = SnowballInstance.GetComponent<Rigidbody>();
        }

        public virtual void Attack()
        {
            var forceDir = startPosition.forward;
            _snowballRb.AddForce(forceDir * force, ForceMode.Impulse);
            
            CleanCache();
        }

        private void CleanCache()
        {
            SnowballInstance = null;
            _snowballRb = null;
        }

        public virtual void UpdateAimingLine() 
        {
            //override
        }
    }
}
