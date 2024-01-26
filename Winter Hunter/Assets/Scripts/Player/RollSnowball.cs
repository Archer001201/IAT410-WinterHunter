using Snowball;
using UnityEngine;

namespace Player
{
    public class RollSnowball : SnowballAttack
    {
        [Header("RollSnowball Component Settings")]
        public GameObject rollingLine;
        [Header("RollSnowball Parameters")]
        public float scaleFactor;
        
        private RollingSnowball _rollingSnowballScript;

        private void OnEnable()
        {
            rollingLine.SetActive(true);
        }

        private void OnDisable()
        {
            rollingLine.SetActive(false);
        }

        public override void CreateSnowball()
        {
            base.CreateSnowball();
            _rollingSnowballScript = SnowballInstance.GetComponent<RollingSnowball>();
        }

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
                PlayerAttr.stamina += stamina;
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
