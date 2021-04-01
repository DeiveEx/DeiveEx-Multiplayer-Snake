using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public abstract class GridCell : MonoBehaviour
    {
        public Vector2Int gridPosition;

        public event EventHandler cellDestroyed;

        public abstract void OnCollision(SnakeCell otherCell);

        public virtual void DestroyCell()
        {
            OnCellDestroyed(EventArgs.Empty);
        }

        protected virtual void OnCellDestroyed(EventArgs args)
        {
            cellDestroyed?.Invoke(this, args);
        }
    }
}