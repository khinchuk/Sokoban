using UnityEngine;

public interface IMovable
{
   public void Push(Vector2 direction);

   public Transform Transform { get; }

}
