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

        private FieldContainer _fieldContainer;
        private InputSystem _input;

        public void Init(FieldContainer fieldContainer, InputSystem input)
        {
            _fieldContainer = fieldContainer;
            _input = input;
            _input.OnInput += OnInput;
        }
        
      
        private void OnInput(Vector2 direction)
        {
            if (_readyToMove)
            {
                _readyToMove = false;
                Move(direction);
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
            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + direction;
            foreach (var tile in _fieldContainer.Tiles)
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

            foreach (IMovable movable in _fieldContainer.Movables)
            {
                Transform movableTransform = ((MonoBehaviour)movable).transform; 
                if (movableTransform.position.x == newPosition.x && movableTransform.position.y == newPosition.y)
                {
                    Vector2 positionBehindMovable = newPosition + direction;

                    foreach (var tile in _fieldContainer.Tiles)
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

                    foreach (var item in _fieldContainer.Movables)
                    {
                        Transform itemTransform = ((MonoBehaviour)item).transform; 
                        if (itemTransform.position.x == positionBehindMovable.x && itemTransform.position.y == positionBehindMovable.y)
                        {
                            _readyToMove = true;
                            return false;
                        }
                    }
                    movable.Push(direction);
                    return true;
                }
            }
          
            return true;
        }
      
        private void OnDestroy()
        {
            _input.OnInput -= OnInput;
        }
    }
}