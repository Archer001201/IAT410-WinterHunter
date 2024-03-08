using System;
using Cinemachine;
using Player;
using UnityEngine;

namespace Utilities
{
    public class CameraController : MonoBehaviour
    {
        public CinemachineVirtualCamera virCamera;
        private Transform _playerTrans;
        private PlayerAttribute _playerAttr;
        public float normalFOV = 30f; 
        public float combatFOV = 45f; 
        private const float LerpSpeed = 2f;

        private void Awake()
        {
            var player = GameObject.FindWithTag("Player");
            _playerTrans = player.transform;
            _playerAttr = player.GetComponent<PlayerAttribute>();
            virCamera.Follow = _playerTrans;
        }

        private void Update()
        {
            float targetFOV = _playerAttr.isInCombat ? combatFOV : normalFOV;
            virCamera.m_Lens.FieldOfView = Mathf.Lerp(virCamera.m_Lens.FieldOfView, targetFOV, LerpSpeed * Time.deltaTime);
        }
    }
}
