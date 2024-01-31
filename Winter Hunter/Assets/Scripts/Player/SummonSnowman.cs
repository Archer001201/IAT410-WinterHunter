using System.Collections.Generic;
using Snowman;
using UnityEngine;

namespace Player
{
    public class SummonSnowman : MonoBehaviour
    {
        public List<SnowmanType> snowmanList;
        public GameObject currentSnowman;

        public Transform startPosition;
        public float summoningCost;

        public int currentIndex;

        private void Awake()
        {
            currentIndex = 0;
            LoadSnowmanPrefab();
        }

        public void SwitchSnowmanLeft()
        {
            if (currentIndex < snowmanList.Count-1) currentIndex++;
            else currentIndex = 0;
            
            LoadSnowmanPrefab();
        }
        
        public void SwitchSnowmanRight()
        {
            if (currentIndex > 0) currentIndex--;
            else currentIndex = snowmanList.Count-1;
            
            LoadSnowmanPrefab();
        }

        public void SummonCurrentSnowman()
        {
            Instantiate(currentSnowman, startPosition.position, startPosition.rotation);
        }

        private void LoadSnowmanPrefab()
        {
            currentSnowman = snowmanList[currentIndex] switch
            {
                SnowmanType.Normal => Resources.Load<GameObject>("Prefabs/Snowman/Proto_Normal"),
                SnowmanType.MeatShield => Resources.Load<GameObject>("Prefabs/Snowman/Proto_MeatShield"),
                SnowmanType.Healer => Resources.Load<GameObject>("Prefabs/Snowman/Proto_Healer"),
                _ => currentSnowman
            };

            if (currentSnowman == null) return;
            summoningCost = currentSnowman.GetComponent<BaseSnowman>().summoningCost;
        }
    }
}
