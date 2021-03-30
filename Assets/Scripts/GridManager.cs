using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Grid;

namespace SnakeGame
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private bool showGrid; //HACK remove later or change to editor only
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float cellsSize = 1;
        [SerializeField] private Vector2 gridOrigin;
        [SerializeField] private WallCell wallPrefab;

        private GenericGrid<GridCell> grid;

        private void Awake()
        {
            grid = new GenericGrid<GridCell>(gridSize.x, gridSize.y, cellsSize, cellsSize, gridOrigin);
            grid.gridValueChanged += RepositionGameObject;
        }

        private void OnDestroy()
        {
            grid.gridValueChanged -= RepositionGameObject;
        }

        private void RepositionGameObject(object sender, GridValueChangedEventArgs e)
        {
            GridCell cell = grid.GetValue(e.x, e.y);
            if (cell != null)
            {
                cell.transform.position = grid.GetWorldCenterPosition(e.x, e.y);
            }
        }

        public void PrepareGrid()
        {
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    if (i == 0 ||
                        i == gridSize.x - 1 ||
                        j == 0 ||
                        j == gridSize.y - 1)
                    {

                        SetValue(i, j, Instantiate(wallPrefab));
                    }
                }
            }
        }

        public Vector2Int GetGridSize()
        {
            return grid.Size;
        }

        public void SetValue(int x, int y, GridCell value)
        {
            GridCell previousCell = grid.GetValue(x, y);

            if (previousCell != null)
            {
                previousCell.cellDestroyed -= CellDestroyedHandler;
            }

            grid.SetValue(x, y, value);

            if (value != null)
            {
                value.gridPosition = new Vector2Int(x, y);
                value.cellDestroyed += CellDestroyedHandler;
            }
        }

        private void CellDestroyedHandler(object sender, EventArgs e)
        {
            Debug.Log($"Destroying {(sender as GridCell).name}");
            Destroy((sender as GridCell).gameObject);
        }

        public GridCell GetValue(int x, int y)
        {
            return grid.GetValue(x, y);
        }

        private void OnDrawGizmos()
        {
            if (grid != null)
            {
                if (showGrid)
                    grid.DrawGrid();
            }
        }
    }
}
