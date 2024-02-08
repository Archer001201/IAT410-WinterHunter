using System;
using System.Collections.Generic;
using Snowball;
using Snowman;
using UnityEngine;
using EventHandler = EventSystem.EventHandler;

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
