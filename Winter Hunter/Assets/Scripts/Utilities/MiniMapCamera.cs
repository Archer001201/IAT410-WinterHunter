using System;
using UnityEngine;

namespace Utilities
{
    public class MiniMapCamera : MonoBehaviour
    {
        private Transform _playerTrans;

        private void Awake()
        {
            _playerTrans = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            transform.position = _playerTrans.position;
        }
    }
}
