using UnityEngine;

namespace Player
{
    /*
     * The action of throwing snowball
     */
    public class ThrowSnowball : SnowballAttack
    {
        private void OnEnable()
        {
            aimingLineRenderer.enabled = true;
        }

        private void OnDisable()
        {
            aimingLineRenderer.enabled = false;
        }

        /*
         * Make snowball move forward and cost player's stamina
         */
        public override void Attack()
        {
            if (PlayerAttr.stamina < Mathf.Abs(stamina)) return;
            CreateSnowball();
            base.Attack();
            PlayerAttr.stamina += stamina;
        }

        /*
         * Update aiming line
         */
        public override void UpdateAimingLine()
        {
            base.UpdateAimingLine();
            var points = new Vector3[lineSegmentCount];
            var startingPosition = startPosition.position;
            var startingVelocity = startPosition.forward * force;

            for (var i = 0; i < lineSegmentCount; i++)
            {
                var t = i / (float)lineSegmentCount;
                points[i] = startingPosition + t * startingVelocity;
                points[i].y = startingPosition.y + t * startingVelocity.y + 0.5f * Physics.gravity.y * t * t;
            }

            aimingLineRenderer.positionCount = lineSegmentCount;
            aimingLineRenderer.SetPositions(points);
        }
    }
}
