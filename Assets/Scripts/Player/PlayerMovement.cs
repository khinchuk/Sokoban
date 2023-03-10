using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace Sokoban
{
    
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _stepSpeed = 0.5f;
        public void Move(Vector2 direction, Action<bool> callback)
        {
            if (Mathf.Abs(direction.x) < 0.5)
            {
                direction.x = 0;
            }
            else
            {
                direction.y = 0;
            }

            if (direction.x > 0.5)
            {
                direction.x = 1;
            }

            if (direction.y > 0.5)
            {
                direction.y = 1;
            }
            direction.Normalize();

            var newPosition = new Vector2(transform.position.x, transform.position.y ) + direction;
            
            bool moveIsFinished = true;
            transform.DOMove(newPosition, _stepSpeed).SetEase(Ease.Linear).OnComplete( () => callback(moveIsFinished));
            Debug.Log($"direction {direction} and position {newPosition}");
        }
       
    }
}