using System;
using Cinemachine;
using Player;
using UnityEngine;

namespace Utilities
{
    public class CameraController : MonoBehaviour
    {
        public CinemachineVirtualCamera virCamera;
        // public Vector3 offset;
        private Transform _playerTrans;
        private PlayerAttribute _playerAttr;
        public float normalFOV = 30f; // 非战斗状态的FOV
        public float combatFOV = 45f; // 战斗状态的FOV
        private const float LerpSpeed = 1f;

        // private Transform _followTrans;

        private void Awake()
        {
            var player = GameObject.FindWithTag("Player");
            _playerTrans = player.transform;
            _playerAttr = player.GetComponent<PlayerAttribute>();
            // _followTrans.position = (_playerTrans.position += offset);
            // // _followTrans.position += offset;
            //
            virCamera.Follow = _playerTrans;
        }

        private void Update()
        {
            float targetFOV = _playerAttr.isInCombat ? combatFOV : normalFOV;
            // Mathf.Lerp函数用于从当前FOV平滑过渡到目标FOV
            virCamera.m_Lens.FieldOfView = Mathf.Lerp(virCamera.m_Lens.FieldOfView, targetFOV, LerpSpeed * Time.deltaTime);
        }
    }
}
