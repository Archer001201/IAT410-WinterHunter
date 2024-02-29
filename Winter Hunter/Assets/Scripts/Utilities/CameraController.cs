using System;
using Cinemachine;
using UnityEngine;

namespace Utilities
{
    public class CameraController : MonoBehaviour
    {
        public CinemachineVirtualCamera virCamera;
        // public Vector3 offset;
        private Transform _playerTrans;
        // private Transform _followTrans;

        private void Awake()
        {
            _playerTrans = GameObject.FindWithTag("Player").transform;
            // _followTrans.position = (_playerTrans.position += offset);
            // // _followTrans.position += offset;
            //
            virCamera.Follow = _playerTrans;
        }
    }
}
