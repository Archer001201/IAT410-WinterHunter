using System;
using UnityEngine;

namespace Snowball
{
    public class ThrowingVfx : MonoBehaviour
    {
        public ParticleSystem vfx;

        private void Update()
        {
            if (vfx.isStopped) Destroy(gameObject);
        }
    }
}
