using System;
using DataSO;
using Props;
using UnityEngine;
using UnityEngine.Playables;

namespace Utilities
{
    public class CutSceneController : MonoBehaviour
    {
        public string id;
        public bool hasWatched;
        private GameObject[] _canvas = new GameObject[]{};
        private PlayableDirector _director;
        private GameSO _gameSO;
        private LevelSO _levelSO;

        private void Awake()
        {
            id = gameObject.name;
            _director = GetComponent<PlayableDirector>();
            
            _gameSO = Resources.Load<GameSO>("DataSO/Game_SO");
            _levelSO = _gameSO.currentGameData.levelSo;
            
            // LoadData();
            // gameObject.SetActive(!hasWatched);
        }

        private void OnEnable()
        {
            LoadData();
            gameObject.SetActive(!hasWatched);
            
            _director.played += OnPlay;
            _director.stopped += OnStop;
        }

        private void OnDisable()
        {
            _director.played -= OnPlay;
            _director.stopped -= OnStop;
        }

        private void OnPlay(PlayableDirector director)
        {
            _canvas = GameObject.FindGameObjectsWithTag("Canvas");
            foreach (var canvas in _canvas)
            {
                canvas.SetActive(false);
            }
            Time.timeScale = 0;  
        }

        private void OnStop(PlayableDirector director)
        {
            foreach (var canvas in _canvas)
            {
                canvas.SetActive(true);
            }
            Time.timeScale = 1;
            hasWatched = true;
            SaveData();
            _director.gameObject.SetActive(false);
        }
        
        private void LoadData()
        {
            var cg = _levelSO.cutScenes.Find(cutScene => cutScene.id == id);
            if (cg == null)
            {
                _levelSO.cutScenes.Add(new BlockWallData
                {
                    id = this.id,
                    isVisible = this.hasWatched
                });
            }
            else
            {
                hasWatched = cg.isVisible;
            }
        }
        
        private void SaveData()
        {
            var cg = _levelSO.cutScenes.Find(cutScene => cutScene.id == id);
            if (cg == null) return;
            cg.isVisible = hasWatched;
        }
    }
}
