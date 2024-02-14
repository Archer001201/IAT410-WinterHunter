using System;
using System.Collections.Generic;
using Snowball;
using Snowman;
using UnityEngine;
using EventHandler = EventSystem.EventHandler;

namespace Props
{
    /*
     * A type of chest that stores snowman data
     */
    public class SnowmanChest : Chest
    {
        public List<SnowmanType> snowmanList;
        
        /*
         * Open chest and notice player that the chest is opened 
         */
        public override void OpenChest()
        {
            EventHandler.OpenSnowmanChest(snowmanList);
            base.OpenChest();
        }
    }
}
