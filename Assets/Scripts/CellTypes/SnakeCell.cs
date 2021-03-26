using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class SnakeCell : GridCell
    {
        public Vector2Int gridPosition;

        private Vector2Int direction;
        private SnakeCell parentCell;

        public SnakeCell(Vector2Int direction, SnakeCell parent)
        {
            color = Color.blue;
            this.direction = direction;
            this.parentCell = parent;
        }

        public Vector2Int GetDirection()
        {
            return direction;
        }

        public SnakeCell GetParent()
        {
            return parentCell;
        }

        public void Rotate(int dir)
        {
            direction = new Vector2Int() {
                x = (int)(direction.x * Mathf.Cos(90 * dir * Mathf.Deg2Rad) - direction.y * Mathf.Sin(90 * dir * Mathf.Deg2Rad)),
                y = (int)(direction.x * Mathf.Sin(90 * dir * Mathf.Deg2Rad) + direction.y * Mathf.Cos(90 * dir * Mathf.Deg2Rad)),
            };
        }
    }
}