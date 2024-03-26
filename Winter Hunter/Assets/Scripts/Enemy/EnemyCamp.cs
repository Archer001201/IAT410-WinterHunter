using System;
using System.Collections;
using System.Collections.Generic;
using DataSO;
using Props;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using EventHandler = Utilities.EventHandler;
using Random = UnityEngine.Random;

namespace Enemy
{
    /*
     * Store data of enemies and chests in the camp
     */
    public class EnemyCamp : MonoBehaviour
    {
        public string id;
        public UnityEvent onCampCleared;
        public List<EnemyWave> enemyWaves;
        public List<TreasureChest> chestList;
        public int waveThreshold;
        public int waveIndex = 0;
        public bool isCleared;
        public bool isPlayerInCamp;
        public GameObject respawnVfx;

        public bool isBossCamp;
        // public GameObject bossCampDoor;
        public List<CampDoor> campDoors;
        
        private LevelSO _levelSo;
        private GameSO _gameSo;
        
        private readonly List<GameObject> _enemiesOnStandby = new();
        public List<BaseEnemy> respawnedEnemy = new ();
        public int enemyAmount;
        private Coroutine _respawnCoroutine;

        private void Awake()
        {
            id = gameObject.name;
            _gameSo = Resources.Load<GameSO>("DataSO/Game_SO");
            _levelSo = _gameSo.currentGameData.levelSo;
            LoadEnemyCampData();

            // foreach (var enemy in enemyList)
            // {
            //     if (!enemy.gameObject.activeSelf) _enemiesOnStandby.Add(enemy.gameObject);
            // }
            // if (gameObject.activeSelf)
            // {
            //     _respawnCoroutine ??= StartCoroutine(RespawnEnemyWave(waveIndex));
            // }

            foreach (var door in campDoors)
            {
                door.enemyCamp = this;
            }

            foreach (var wave in enemyWaves)
            {
                enemyAmount += wave.enemyWave.Count;
            }
        }

        private void OnEnable()
        {
            _respawnCoroutine ??= StartCoroutine(RespawnEnemyWave(waveIndex));
        }

        private void OnDisable()
        {
            if (_respawnCoroutine == null) return;
            StopCoroutine(_respawnCoroutine);
            _respawnCoroutine = null;
        }

        private IEnumerator RespawnEnemyWave(int index)
        {
            if (index >= enemyWaves.Count)
            {
                StopCoroutine(_respawnCoroutine);
                _respawnCoroutine = null;
            }
            foreach (var enemy in enemyWaves[index].enemyWave)
            {
                var campPos = transform.position;
                var xPos = Random.Range(campPos.x - 10f, campPos.x + 10f);
                var zPos = Random.Range(campPos.z - 10f, campPos.z + 10f);
                var enemyPos = new Vector3(xPos, 0, zPos);
                Instantiate(respawnVfx, enemyPos, Quaternion.identity);
                yield return new WaitForSeconds(1.5f);
                var currentEnemy = Instantiate(enemy, enemyPos, Quaternion.identity);
                var enemyScript = currentEnemy.GetComponent<BaseEnemy>();
                respawnedEnemy.Add(enemyScript);
                yield return new WaitForSeconds(1.5f);
                if (index > 0) InitializeEnemy(enemyScript);
            }

            waveIndex++;
            
            StopCoroutine(_respawnCoroutine);
            _respawnCoroutine = null;
        }

        private void UpdateEnemyWave()
        {
            for (var i = 0; i < respawnedEnemy.Count; i++)
            {
                var enemy = respawnedEnemy[i];
                if (enemy == null)
                {
                    respawnedEnemy.RemoveAt(i);
                    enemyAmount--;
                    return;
                }
            }

            if (respawnedEnemy.Count <= waveThreshold && waveIndex < enemyWaves.Count)
            {
                _respawnCoroutine ??= StartCoroutine(RespawnEnemyWave(waveIndex));
            }
        }

        private void Update()
        {
            if (enemyAmount < 1 && !isCleared)
            {
                isCleared = true;
                onCampCleared?.Invoke();
                foreach (var t in chestList)
                {
                    t.canOpen = isCleared;
                }
                EventHandler.ChangePlayerBattleState(false);
                SaveData();
            }
            //
            // for (var i = 0; i < enemyList.Count; i++)
            // {
            //     if (enemyList[i] == null) enemyList.Remove(enemyList[i]);
            // }
            
            UpdateEnemyWave();
        }

        public void NotifyEnemiesToChangeChasingState()
        {
            foreach (var enemy in respawnedEnemy)
            {
                InitializeEnemy(enemy);
            }

            foreach (var door in campDoors)
            {
                door.vfx.SetActive(true);
            }
        }

        private void InitializeEnemy(BaseEnemy enemy)
        {
            enemy.isChasing = true;
            enemy.SetTarget();
            EventHandler.AddEnemyToCombatList(enemy.gameObject);
        }

        // private void UpdateEnemyWave()
        // {
        //     var activatedAmount = enemyList.Count - _enemiesOnStandby.Count;
        //     if (activatedAmount > waveThreshold) return;
        //     for (var i = 0; i < _enemiesOnStandby.Count; i++)
        //     {
        //         if (i >= enemiesPerWave) return;
        //         var enemy = _enemiesOnStandby[i];
        //         enemy.SetActive(true);
        //         _enemiesOnStandby.RemoveAt(i);
        //     }
        // }
        
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
