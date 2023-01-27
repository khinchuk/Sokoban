using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sokoban.Service;
using UnityEngine;
using Zenject;


namespace Sokoban
{
    public class ScoreService : MonoBehaviour
    {
        [Inject] private FieldContainer _fieldContainer;
        [Inject] private SoundService _soundService;

        
        private Tile[] _tiles;
        private Tile[] _goalTiles;
        private IMovable[] _movables;
        private List<Tile> _goalsList = new List<Tile>();
        
        public Action OnAllGoalsResolved;

        private void Start()
        {
            DontDestroyOnLoad(this);
            _fieldContainer.OnEnvironmentContainFinifhed += Init;
        }

        private void Init()
        {
            if (_fieldContainer != null)
            {
                _tiles = _fieldContainer.Tiles;
                Debug.Log($"_fieldContainer.Tiles {_fieldContainer.Tiles.Length}");
                foreach (var tile in _tiles)
                {
                    if (tile.TileState == TileState.Goal)
                    {
                        Debug.Log($"_tiles ::::TileState.Goal");
                        _goalsList.Add(tile);
                    }
                }
                // var array = (Tile[]) _tiles.Where(x => x.TileState == TileState.Goal);
                // _goalTiles = array;
            }
            else
            {
                Debug.Log($"_fieldContainer nulll !!!!!!!!!");

            }
           
            Debug.Log($"_GoalsList {_goalsList.Count}");
            _movables = _fieldContainer.Movables;
            
            if (IsAllGoalsResolved())
            {
                _soundService.PlaySound(SFXType.AllGoals);
            }
            
        }
        

        public  bool IsAllGoalsResolved()
        {
            if (_goalsList == null)
            {
                Debug.Log($"_goalsList nulll !!!!!!!!!");
                return false;

            }
            for ( int i = 0; i < _goalsList.Count; i++)
            {
               
                for (int j = 0; j < _movables.Length; j++)
                {
                    if (_goalsList[i].transform.position == _movables[j].Transform.position)
                    {
                        Debug.Log($"_goalsList {i} ==  _movables {j}  ");
                        break;
                    }

                    if (_goalsList[i].transform.position != _movables[_movables.Length-1].Transform.position)
                    {
                        Debug.Log($"_goalsList {i} !=  _movables {_movables.Length-1}  ");

                        return false;
                    }
                }
            }

            OnAllGoalsResolved?.Invoke();
            return true;
        }

        private void SaveTime(string time)
        {
            
        }
    }
}