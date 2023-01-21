using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sokoban
{
    
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private Tile[] _tiles;
        private Movable[] _movables;
        private FieldManager _fieldManager;

        public Tile[] Tiles => _tiles;
        public Movable[] Movables => _movables;
        
        private void Start()
        {
            _tiles = FindObjectsOfType<Tile>();
            _movables = FindObjectsOfType<Movable>();
            _player.InitPlayer(this);
        }

       
        
        
    }
}