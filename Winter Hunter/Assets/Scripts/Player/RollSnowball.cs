using System;
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
        public float attackBonusFactor;
        [Header("RollSnowball Parameters")]
        public float scaleFactor;
        public float staminaIncrease;
        public bool showRollingLine;
        private float _attackBonus;
        
        private RollingSnowball _rollingSnowballScript;

        private void Update()
        {
            if (showRollingLine) UpdateRollingLine();
        }


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
                // Debug.Log("snowball null");
                enabled = false;
                return;
            }
            
            // _rollingSnowballScript.SetAttack(PlayerAttr.attack + _attackBonus);
            _rollingSnowballScript.SetReleasingState();
            base.Attack();
            _attackBonus = 0;
            // Debug.Log(PlayerAttr.stamina);
        }

        /*
         * Scale snowball and cost stamina, also update the aiming line
         */
        public void UpdateSnowball(Vector3 moveDir)
        {
            if (SnowballInstance == null)
            {
                // enabled = false;
                return;
            }
            // UpdateRollingLine();
            if (moveDir != Vector3.zero)
            {
                var scaleIncrease = new Vector3(scaleFactor, scaleFactor, scaleFactor) * Time.fixedDeltaTime;
                SnowballInstance.transform.localScale += scaleIncrease;
                _attackBonus += (attackBonusFactor * PlayerAttr.attack * Time.fixedDeltaTime);
                _rollingSnowballScript.SetAttack(PlayerAttr.attack + _attackBonus);

                if (PlayerAttr.stamina >= Mathf.Abs(staminaIncrease * Time.fixedDeltaTime))
                {
                    PlayerAttr.stamina += staminaIncrease * Time.fixedDeltaTime;
                }
                else
                {
                    Attack();
                }
            }

            if (SnowballInstance == null) return;
            SnowballInstance.transform.position = startPosition.position;
            SnowballInstance.transform.rotation = startPosition.rotation;
            
        }

        private void UpdateRollingLine()
        {
            if (!rollingLine.activeInHierarchy) rollingLine.SetActive(true);
            // const float distance = RollingSnowball.RollingDistance - 5f;
            rollingLine.transform.localScale = new Vector3(PlayerAttr.transform.localScale.x,1,10f);
            rollingLine.transform.position = startPosition.position;
            rollingLine.transform.rotation = startPosition.rotation;
            // Debug.Log("line");
        }
    }
}
