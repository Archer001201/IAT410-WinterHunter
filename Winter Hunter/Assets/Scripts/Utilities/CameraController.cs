using System;
using Cinemachine;
using UnityEngine;

namespace Utilities
{
    public class CameraController : MonoBehaviour
    {
        public CinemachineVirtualCamera virCamera;

        private void Awake()
        {
            virCamera.Follow = GameObject.FindWithTag("Player").transform;
        }
    }
}
