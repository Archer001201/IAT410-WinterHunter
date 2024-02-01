using System.Collections.Generic;
using DataSO;
using Snowman;
using UnityEngine;

namespace Player
{
    public class SummonSnowman : MonoBehaviour
    {
        // public List<SnowmanType> snowmanList;
        public GameObject currentSnowman;

        public Transform startPosition;
        public float summoningCost;

        public int currentIndex;

        private PlayerSO _playerSO;

        private void Awake()
        {
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            currentIndex = 0;
            LoadSnowmanPrefab();
        }

        public void SwitchSnowmanLeft()
        {
            if (currentIndex < _playerSO.snowmanList.Count-1) currentIndex++;
            else currentIndex = 0;
            
            LoadSnowmanPrefab();
        }
        
        public void SwitchSnowmanRight()
        {
            if (currentIndex > 0) currentIndex--;
            else currentIndex = _playerSO.snowmanList.Count-1;
            
            LoadSnowmanPrefab();
        }

        public void SummonCurrentSnowman()
        {
            Instantiate(currentSnowman, startPosition.position, startPosition.rotation);
        }

        private void LoadSnowmanPrefab()
        {
            if (_playerSO.snowmanList.Count < 1) return;
            currentSnowman = _playerSO.snowmanList[currentIndex] switch
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
