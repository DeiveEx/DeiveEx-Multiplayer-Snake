using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Grid;

namespace SnakeGame
{
    public class GridManager : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool showGrid;
#endif
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float cellsSize = 1;
        [SerializeField] private Vector2 gridOrigin;
        [SerializeField] private WallCell wallPrefab;

        private GenericGrid<GridCell> grid;

        private void Awake()
        {
            GenerateGrid(gridSize.x, gridSize.y, cellsSize, cellsSize, gridOrigin);
        }

        private void OnDestroy()
        {
            if (grid != null)
                grid.gridValueChanged -= RepositionGameObject;
        }

        public void GenerateGrid(int width, int height, float cellWidth, float cellHeight, Vector3 gridOrigin)
        {
            if (grid != null)
                grid.gridValueChanged -= RepositionGameObject;

            grid = new GenericGrid<GridCell>(width, height, cellWidth, cellHeight, gridOrigin);
            grid.gridValueChanged += RepositionGameObject;
        }

        private void RepositionGameObject(object sender, GridValueChangedEventArgs e)
        {
            GridCell cell = grid.GetValue(e.x, e.y);
            if (cell != null)
            {
                cell.transform.position = grid.GetWorldCenterPosition(e.x, e.y);
            }
        }

        public void GenerateWalls()
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
                        GridCell wall = Instantiate(wallPrefab);
                        wall.transform.SetParent(transform);
                        SetValue(i, j, wall);
                    }
                }
            }
        }

        public Vector2Int GetRandomPosition(int margin)
        {
            return new Vector2Int() {
                x = Random.Range(margin, grid.Size.x - margin),
                y = Random.Range(margin, grid.Size.y - margin),
            };
        }

        public Vector2Int GetGridSize()
        {
            return grid.Size;
        }

        public void SetValue(int x, int y, GridCell cell)
        {
            GridCell previousCell = grid.GetValue(x, y);

            if (previousCell != null)
            {
                previousCell.cellDestroyed -= Cell_cellDestroyed;
            }

            grid.SetValue(x, y, cell);

            if (cell != null)
            {
                cell.gridPosition = new Vector2Int(x, y);
                cell.cellDestroyed += Cell_cellDestroyed;
            }
        }

        private void Cell_cellDestroyed(object sender, System.EventArgs e)
        {
            Destroy((sender as GridCell).gameObject);
        }

        public GridCell GetValue(int x, int y)
        {
            return grid.GetValue(x, y);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (grid != null)
            {
                if (showGrid)
                    grid.DrawGrid();
            }
        }
#endif
    }
}
