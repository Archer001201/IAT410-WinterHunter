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
        public float raycastDistance;
        public bool isCleared;
        public bool isPlayerInTheCamp;

        public bool isBossCamp;
        public GameObject bossCampDoor;
        public List<CampDoor> campDoors;
        
        private LevelSO _levelSo;
        private PlayerSO _playerSo;
        private GameSO _gameSo;

        private GameObject _player;
        private readonly List<GameObject> _enemiesOnStandby = new();

        private void Awake()
        {
            id = gameObject.name;
            // levelSo.enemyCamps.Add(gameObject);
            _player = GameObject.FindWithTag("Player");
            _gameSo = Resources.Load<GameSO>("DataSO/Game_SO");
            _playerSo = _gameSo.currentGameData.playerSo;
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
            
            // CheckPlayerInRaycastRange();
            UpdateEnemyWave();
        }

        // private void CheckPlayerInRaycastRange()
        // {
        //     if (isPlayerInTheCamp) return;
        //     var dir = _player.transform.position - transform.position;
        //
        //     var layerMask =  1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Player");
        //
        //     isPlayerInTheCamp =
        //         Physics.Raycast(transform.position, dir.normalized, out var hit, raycastDistance, layerMask) &&
        //         hit.collider.CompareTag("Player");
        //     
        //     if (isPlayerInTheCamp)
        //     {
        //         // NotifyEnemiesToChangeChasingState(true);
        //     }
        //     // NotifyEnemiesToChangeChasingState(
        //     //     Physics.Raycast(transform.position, dir.normalized, out var hit, raycastDistance, layerMask) &&
        //     //     hit.collider.CompareTag("Player"));
        //     
        //     Debug.DrawLine(transform.position, _player.transform.position, Color.blue);
        // }

        public void NotifyEnemiesToChangeChasingState()
        {
            foreach (var enemy in enemyList)
            {
                // if (enemy.isPlayerInCampRange == isPlayerInCampRange) return;
                // enemy.isPlayerInCampRange = isPlayerInCampRange;
                // enemy.SetChaseTarget();
                // if (isChasing && enemy.CurrentState != enemy.ChaseState)
                //     enemy.SwitchState(EnemyState.Chase);
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
                // Debug.LogError("No camp data");
                return;
            }
            
            camp.isCleared = isCleared;
            _gameSo.SaveData();
            foreach (var chest in chestList)
            {
                chest.SaveCanOpenState();
            }
            transform.parent.gameObject.SetActive(false);
            // Debug.Log("Data saved");
        }
    }
}
