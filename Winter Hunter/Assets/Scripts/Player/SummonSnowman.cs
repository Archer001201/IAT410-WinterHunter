using System;
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

        private int _currentIndex;

        private void Awake()
        {
            _currentIndex = 0;
            LoadSnowmanPrefab();
        }

        public void SwitchSnowman()
        {
            if (_currentIndex < snowmanList.Count-1) _currentIndex++;
            else _currentIndex = 0;
            
            LoadSnowmanPrefab();
        }

        public void SummonCurrentSnowman()
        {
            Instantiate(currentSnowman, startPosition.position, startPosition.rotation);
        }

        private void LoadSnowmanPrefab()
        {
            currentSnowman = snowmanList[_currentIndex] switch
            {
                SnowmanType.Normal => Resources.Load<GameObject>("Prefabs/Snowman/Proto_Normal"),
                SnowmanType.MeatShield => Resources.Load<GameObject>("Prefabs/Snowman/Proto_MeatShield"),
                _ => currentSnowman
            };
        }
    }
}
