using System;
using Snowman;

namespace Player
{
    /*
     * Snowman summoning information
     */
    [Serializable]
    public class SnowmanInfor
    {
        public SnowmanType type;
        public float cooldown;
        public float cooldownTimer;
        public float summoningCost;
        public bool canBeSummoned;
    }
}
