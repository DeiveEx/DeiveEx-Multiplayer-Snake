using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class SnakeSegmentEventArgs : EventArgs
    {
        public SnakeCell segmentToAdd;
    }

    public class SnakeCell : GridCell
    {
        public Vector2Int Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector2)value);
            }
        }
        public SnakeCell parentCell;

        public event EventHandler<SnakeSegmentEventArgs> itemConsumed;

        private Vector2Int _direction;
        protected int rotationDirection;

        public SnakeCell GetParent()
        {
            return parentCell;
        }

        public bool CheckCollision(GridManager gridManager)
        {
            Vector2Int newPosition = gridPosition + Direction;
            GridCell destinationCell = gridManager.GetValue(newPosition.x, newPosition.y);

            if (destinationCell != null)
            {
                Debug.Log($"Collision with {destinationCell.name} on {newPosition}");
                destinationCell.OnCollision(this);
            }

            return destinationCell != null;
        }

        public void OnItemConsumed(SnakeCell segmentToAdd)
        {
            itemConsumed?.Invoke(this, new SnakeSegmentEventArgs() { segmentToAdd = segmentToAdd });
        }

        public void MoveForward(GridManager gridManager)
        {
            Vector2Int oldPosition = gridPosition;
            Vector2Int newPosition = gridPosition + Direction;

            gridManager.SetValue(newPosition.x, newPosition.y, this);
            gridManager.SetValue(oldPosition.x, oldPosition.y, null);
        }

        public void SetRotationDirection(int dir)
        {
            rotationDirection = dir;
        }

        public void UpdateDirection()
        {
            Rotate(rotationDirection);
            rotationDirection = 0;
        }

        protected void Rotate(int dir)
        {
            if (dir == 0)
                return;

            dir = (int)Mathf.Sign(dir);

            transform.Rotate(0, 0, 90 * dir);
            Direction = new Vector2Int(Mathf.RoundToInt(transform.up.x), Mathf.RoundToInt(transform.up.y));
        }

        public override void OnCollision(SnakeCell otherCell)
        {
            otherCell.DestroyCell();
        }

        public virtual void ExecuteSegmentAddedEffect(SnakeController controller) { }
    }
}