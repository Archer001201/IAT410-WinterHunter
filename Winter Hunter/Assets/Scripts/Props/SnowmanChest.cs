using System.Collections.Generic;
using Snowman;
using Utilities;
using EventHandler = EventSystem.EventHandler;

namespace Props
{
    /*
     * A type of chest that stores snowman data
     */
    public class SnowmanChest : Chest
    {
        public List<SnowmanTypeAndLevel> snowmanList;
        
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
