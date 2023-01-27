using System;
using System.Collections;
using System.Collections.Generic;
using Sokoban;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
   [SerializeField] private LevelsContainerScriiptable _levelsContainer;
   [SerializeField] private GameObject _playerPrefab;
   [Inject] private FieldContainer _fieldContainer;
   [Inject] private InputSystem _input;

   private int _currentLevelIndex = 0;
   private GameObject[] _levelsPrefabs;
   private Level _currentLevel;

   public Vector2 LevelPlayerEntryPoint { get; private set; }
   private void Start()
   {
      _levelsPrefabs = _levelsContainer._levelsPrefabs;
      LoadLevel(0);
   }

   public void LoadLevel(int index)
   {
      var prefab = _levelsPrefabs[index];
      _currentLevel = Instantiate(prefab).GetComponent<Level>();
      LevelPlayerEntryPoint = _currentLevel.EntryPoint;
      _fieldContainer.ContainAllLevelEnvironment();
      CreatePlayer(LevelPlayerEntryPoint);
   }

   private void CreatePlayer(Vector2 entryPoint)
   {
      var player = Instantiate(_playerPrefab);
      player.transform.position = (Vector3)LevelPlayerEntryPoint;
      player.GetComponent<Player>().Init(_fieldContainer, _input);
   }
}
