using System.Collections.Generic;
using DataSO;
using Props;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utilities;

namespace Enemy
{
    /*
     * Store data of enemies and chests in the camp
     */
    public class EnemyCamp : MonoBehaviour
    {
        public string id;
        public UnityEvent onCampCleared;
        public List<BaseEnemy> enemyList;
        public List<TreasureChest> chestList;
        public int waveThreshold;
        public int enemiesPerWave;
        public bool isCleared;

        public bool isBossCamp;
        public GameObject bossCampDoor;
        public List<CampDoor> campDoors;
        
        private LevelSO _levelSo;
        private GameSO _gameSo;
        
        private readonly List<GameObject> _enemiesOnStandby = new();

        private void Awake()
        {
            id = gameObject.name;
            _gameSo = Resources.Load<GameSO>("DataSO/Game_SO");
            _levelSo = _gameSo.currentGameData.levelSo;
            LoadEnemyCampData();

            foreach (var enemy in enemyList)
            {
                if (!enemy.gameObject.activeSelf) _enemiesOnStandby.Add(enemy.gameObject);
            }

            foreach (var door in campDoors)
            {
                door.enemyCamp = this;
            }
        }

        private void Update()
        {
            if (enemyList.Count < 1 && !isCleared)
            {
                isCleared = true;
                onCampCleared?.Invoke();
                foreach (var t in chestList)
                {
                    t.canOpen = isCleared;
                }
                SaveData();
            }
            
            for (var i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null) enemyList.Remove(enemyList[i]);
            }
            
            UpdateEnemyWave();
        }

        public void NotifyEnemiesToChangeChasingState()
        {
            foreach (var enemy in enemyList)
            {
                enemy.isChasing = true;
                enemy.SetTarget();
            }

            foreach (var door in campDoors)
            {
                door.vfx.SetActive(true);
            }

            if (isBossCamp)
            {
                bossCampDoor.SetActive(true);
            }
        }

        private void UpdateEnemyWave()
        {
            var activatedAmount = enemyList.Count - _enemiesOnStandby.Count;
            if (activatedAmount > waveThreshold) return;
            for (var i = 0; i < _enemiesOnStandby.Count; i++)
            {
                if (i >= enemiesPerWave) return;
                var enemy = _enemiesOnStandby[i];
                enemy.SetActive(true);
                _enemiesOnStandby.RemoveAt(i);
            }
        }
        
        private void LoadEnemyCampData()
        {
            var camp = _levelSo.enemyCamps.Find(camp => camp.id == id);
            if (camp == null)
            {
                _levelSo.enemyCamps.Add(new CampData
                {
                    id = this.id,
                    isCleared = this.isCleared
                });
            }
            else
            {
                transform.parent.gameObject.SetActive(!camp.isCleared);
            }
        }

        private void SaveData()
        {
            var camp = _levelSo.enemyCamps.Find(camp => camp.id == id);
            if (camp == null)
            {
                return;
            }
            
            camp.isCleared = isCleared;
            _gameSo.SaveData();
            foreach (var chest in chestList)
            {
                chest.SaveCanOpenState();
            }
            transform.parent.gameObject.SetActive(false);
        }
    }
}
