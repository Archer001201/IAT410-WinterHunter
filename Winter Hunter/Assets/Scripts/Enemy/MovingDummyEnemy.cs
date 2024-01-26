using System;
using UnityEngine;

namespace Enemy
{
    public class MovingDummyEnemy : BasicEnemy
    {
        public Transform leftPoint;
        public Transform rightPoint;
        public float speed;
        private Rigidbody _rb;
        private float _time;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // PingPong between 0 and 1
            _time += speed * Time.fixedDeltaTime;
            float t = Mathf.PingPong(_time, 1);

            // Calculate the next position
            Vector3 nextPosition = Vector3.Lerp(leftPoint.position, rightPoint.position, t);
            // Debug.Log(nextPosition);

            // Move the Rigidbody to the next position
            _rb.MovePosition(nextPosition);
        }
    }
}

