using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Sokoban
{
    
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        private bool _readyToMove = true;

        [Inject] private FieldManager _fieldManager;
        

        private void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            input.Normalize();
            Debug.Log($"input {input} and _readyToMove {_readyToMove}");

            if (input.sqrMagnitude > 0.5 && _readyToMove)
            {
                _readyToMove = false;
                Move(input);
            }
        }

        private void Move(Vector2 direction)
        {
            if (CanMove(direction))
            {
                _movement.Move(direction, result => _readyToMove = result);
            }
        }

        private bool CanMove(Vector2 direction)
        {
            Debug.Log($"CanMove direction {direction} ");

            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + direction;
            foreach (var tile in _fieldManager.Tiles)
            {
                if (tile.transform.position.x == newPosition.x && tile.transform.position.y == newPosition.y)
                {
                    if (tile.TileState == TileState.Wall)
                    {
                        _readyToMove = true;
                        return false;
                    }
                }
            }
            
            foreach (var movable in _fieldManager.Movables)
            {
                if (movable.transform.position.x == newPosition.x && movable.transform.position.y == newPosition.y)
                {
                    Vector2 positionBehindMovable = newPosition + direction;

                    foreach (var tile in _fieldManager.Tiles)
                    {
                        if (tile.transform.position.x == positionBehindMovable.x && tile.transform.position.y == positionBehindMovable.y)
                        {
                            if (tile.TileState == TileState.Wall)
                            {
                                _readyToMove = true;
                                return false;
                            }
                        }
                    }

                    foreach (var item in _fieldManager.Movables)
                    {
                        if (item.transform.position.x == positionBehindMovable.x && item.transform.position.y == positionBehindMovable.y)
                        {
                            _readyToMove = true;
                            return false;
                        }
                    }
                   
                }
            }
          
            return true;
        }
      
    }
}