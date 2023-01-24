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
        [SerializeField] private float _treshthold;
        private bool _readyToMove = true;

        [Inject] private FieldContainer _fieldContainer;
        [Inject] private InputSystem _input;


        private void OnEnable()
        {
            _input.OnInput += OnInput;
        }

        // private void Update()
        // {
        //     Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //     Vector2 direction = Vector2.zero;
        //     if (input.x > _treshthold || input.x < -_treshthold)
        //     {
        //         if (input.x > 0)
        //         {
        //             direction = new Vector2(1, 0);
        //         }
        //         else if (input.x < 0)
        //         {
        //             direction = new Vector2(-1, 0);
        //         }
        //             
        //     }
        //     else if (input.y > _treshthold || input.y < -_treshthold)
        //     {
        //         if (input.y > 0)
        //         {
        //             direction = new Vector2(0, 1);
        //         }
        //         else if (input.y < 0)
        //         {
        //             direction = new Vector2(0, -1);
        //         }
        //     }
        //
        //     if (_readyToMove)
        //     {
        //         _readyToMove = false;
        //         Move(direction);
        //     }
        // }

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
      
    }
}