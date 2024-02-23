using System;
using UnityEngine;
using Utilities;

namespace Snowman
{
    [Serializable]
    public class SnowmanInfo
    {
        public Enums.SnowmanType type;
        public Enums.SnowmanLevel level;
        public float cooldown;
        public float cooldownTimer;
        public float summoningCost;
        public bool canBeSummoned;
    }
}
