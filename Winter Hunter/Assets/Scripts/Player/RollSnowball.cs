using Snowball;
using UnityEngine;

namespace Player
{
    /*
     * The action of rolling snowball
     */
    public class RollSnowball : SnowballAttack
    {
        [Header("RollSnowball Component Settings")]
        public GameObject rollingLine;
        [Header("RollSnowball Parameters")]
        public float scaleFactor;
        public float staminaIncrease;
        
        private RollingSnowball _rollingSnowballScript;

        private void OnDisable()
        {
            rollingLine.SetActive(false);
        }

        /*
         * Create rolling snowball
         */
        public override void CreateSnowball()
        {
            if (PlayerAttr.stamina < Mathf.Abs(stamina)) return;
            
            base.CreateSnowball();
            rollingLine.SetActive(true);
            _rollingSnowballScript = SnowballInstance.GetComponent<RollingSnowball>();
            PlayerAttr.stamina += stamina;
        }

        /*
         * Make snowball move forward
         */
        public override void Attack()
        {
            if (SnowballInstance == null)
            {
                enabled = false;
                return;
            }
            
            _rollingSnowballScript.SetReleasingState();
            base.Attack();
        }

        /*
         * Scale snowball and cost stamina, also update the aiming line
         */
        public void UpdateSnowball(Vector3 moveDir)
        {
            if (SnowballInstance == null)
            {
                enabled = false;
                return;
            }
            
            if (moveDir != Vector3.zero)
            {
                var scaleIncrease = new Vector3(scaleFactor, scaleFactor, scaleFactor) * Time.fixedDeltaTime;
                SnowballInstance.transform.localScale += scaleIncrease;
                PlayerAttr.stamina += staminaIncrease;
            }
            SnowballInstance.transform.position = startPosition.position;
            SnowballInstance.transform.rotation = startPosition.rotation;
            UpdateRollingLine();
        }

        private void UpdateRollingLine()
        {
            var distance = _rollingSnowballScript.rollingDistance - 5f;
            rollingLine.transform.localScale = new Vector3(SnowballInstance.transform.localScale.x,1,distance);
            rollingLine.transform.position = startPosition.position;
            rollingLine.transform.rotation = startPosition.rotation;
        }
    }
}
