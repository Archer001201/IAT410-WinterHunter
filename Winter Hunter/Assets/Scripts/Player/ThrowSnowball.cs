using UnityEngine;

namespace Player
{
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

        public override void Attack()
        {
            CreateSnowball();
            base.Attack();
            PlayerAttr.stamina += stamina;
        }

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
