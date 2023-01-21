using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sokoban
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileState _state;
        
        public TileState TileState => _state;
    }

    public enum TileState
    {
        Wall,
        Floor,
        Goal
    }

}