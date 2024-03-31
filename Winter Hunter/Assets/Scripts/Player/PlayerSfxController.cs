using System;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerSfxController : MonoBehaviour
    {
        public AudioSource throwSfx;
        public AudioSource dashSfx;

        public void PlayAudio(PlayerSfxType type)
        {
            switch (type)
            {
                case PlayerSfxType.Throw:
                    throwSfx.Play();
                    break;
                case PlayerSfxType.Dash:
                    dashSfx.Play();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
