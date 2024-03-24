using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class FlameRays : MonoBehaviour
    {
        // public BaseEnemy enemyAttr;
        private Transform _enemyTrans;
        private float _rotateAngle;
        private float _attack;

        private void Awake()
        {
            if (Random.Range(0, 1) < 0.5) _rotateAngle = 60f;
            else _rotateAngle = -60f;
        }

        private void FixedUpdate()
        {
            // var yRotation = -enemyAttr.transform.rotation.y;
            // // transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            transform.position = _enemyTrans.position;
            transform.Rotate(0f, _rotateAngle * Time.fixedDeltaTime, 0f);
        }

        public void SetFlameRays(Transform followedTrans, float attack)
        {
            _enemyTrans = followedTrans;
            _attack = attack;
        }

        public void DestroyMe()
        {
            Destroy(gameObject);
        }

        public float GetAttack()
        {
            return _attack;
        }
    }
}
