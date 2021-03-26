using SnakeGame.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SnakeGame
{
    public class GameManager : MonoBehaviour
    {
        public bool showGrid; //HACK remove later or change to editor only
        public Vector2Int gridSize;
        public float cellsSize = 1;
        public Vector2 gridOrigin;
        public SnakeController playerPrefab;

        private GenericGrid<GridCell> grid;
        private SnakeController player;

        private void Awake()
        {
            PrepareGrid();

            player = Instantiate(playerPrefab);

            Vector2Int startPos = new Vector2Int() {
                x = grid.Size.x / 2,
                y = grid.Size.y / 2
            };

            Vector2Int startDirection = Vector2Int.up;

            player.SetStartingBody(startPos, grid, new List<SnakeCell>() {
                new SnakeCell(startDirection, null),
            });
        }

        private void PrepareGrid()
        {
            grid = new GenericGrid<GridCell>(gridSize.x, gridSize.y, cellsSize, cellsSize, gridOrigin);

            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    if (i == 0 ||
                        i == gridSize.x - 1 ||
                        j == 0 ||
                        j == gridSize.y - 1)
                    {
                        grid.SetValue(i, j, new WallCell());
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (grid != null)
            {
                if (showGrid)
                    grid.DrawGrid();

                for (int i = 0; i < gridSize.x; i++)
                {
                    for (int j = 0; j < gridSize.y; j++)
                    {
                        GridCell cell = grid.GetValue(i, j);

                        if (cell != null)
                        {
                            Gizmos.color = cell.color;
                            Gizmos.DrawCube(grid.GetWorldCenterPosition(i, j), Vector3.one * cellsSize);
                        }
                    }
                }
            }
        }
    }
}