using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Utilities
{
    public class AudioController : MonoBehaviour
    {
        public AudioClip normalBgm;
        public AudioClip combatBgm;
        private PlayerAttribute _playerAttr;
        private AudioSource _audioSource;

        private void Awake()
        {
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_playerAttr.isInCombat)
            {
                if (_audioSource.clip != combatBgm)
                {
                    _audioSource.clip = combatBgm;
                    _audioSource.Play();
                }
                    
            }
            else
            {
                if (_audioSource.clip != normalBgm)
                {
                    _audioSource.clip = normalBgm;
                    _audioSource.Play();
                }
            }
        }
    }
}
