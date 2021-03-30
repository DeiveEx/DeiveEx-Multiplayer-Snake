using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class SnakeCell : GridCell
    {
        public Vector2Int direction;
        public SnakeCell parentCell;

        private int rotationDirection;

        public SnakeCell GetParent()
        {
            return parentCell;
        }

        public bool CheckCollision(GridManager gridManager)
        {
            Vector2Int newPosition = gridPosition + direction;
            GridCell destinationCell = gridManager.GetValue(newPosition.x, newPosition.y);

            if (destinationCell != null)
            {
                destinationCell.OnCollision(this);
            }

            return destinationCell != null;
        }

        public void MoveForward(GridManager gridManager)
        {
            Rotate(rotationDirection);

            Vector2Int oldPosition = gridPosition;
            Vector2Int newPosition = gridPosition + direction;

            gridManager.SetValue(newPosition.x, newPosition.y, this);
            gridManager.SetValue(oldPosition.x, oldPosition.y, null);
        }

        public void SetRotationDirection(int dir)
        {
            rotationDirection = dir;
        }

        private void Rotate(int dir)
        {
            if (dir == 0)
                return;

            dir = (int)Mathf.Sign(dir);

            transform.Rotate(0, 0, 90 * dir);
            direction = new Vector2Int(Mathf.RoundToInt(transform.up.x), Mathf.RoundToInt(transform.up.y));
        }

        public override void OnCollision(GridCell otherCell)
        {
            throw new System.NotImplementedException();
        }
    }
}