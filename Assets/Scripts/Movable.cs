using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sokoban
{
    
    public class Movable : MonoBehaviour, IMovable
    {

        public bool Move(Vector2 direction)
        {
            return false;
        }
    }

}
