using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sokoban
{
    
    public class FieldContainer : MonoBehaviour
    {
        private Tile[] _tiles;
        private IMovable[] _movables;
        private FieldContainer _fieldContainer;

        public Tile[] Tiles => _tiles;
        public IMovable[] Movables => _movables;
        
        private void Start()
        {
            ContainAllLevelEnvironment();
        }

        private void ContainAllLevelEnvironment()
        {
            _tiles = FindObjectsOfType<Tile>();
            _movables = FindObjectsOfType<Chest>();
        }

        private void OnDisable()
        {
            _tiles = null;
            _movables = null;
        }
    }
}