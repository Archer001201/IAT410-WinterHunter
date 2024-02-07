using System.Collections.Generic;
using EventSystem;
using Snowball;
using Snowman;
using UnityEngine;

namespace Props
{
    public class SnowmanChest : Chest
    {
        public List<SnowmanType> snowmanList;

        public override void PickUp()
        {
            EventHandler.OpenSnowmanChest(snowmanList);
            base.PickUp();
        }
    }
}
