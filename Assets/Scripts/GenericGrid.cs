using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class GridValueChangedEventArgs
    {
        public int x;
        public int y;
    }

    public class GenericGrid<T>
    {
        public Vector2Int Size { get; }
        public Vector2 CellSize { get; }
        public Vector3 Origin { get; }

        public event EventHandler<GridValueChangedEventArgs> gridValueChanged;

        private T[,] gridArray;

        public GenericGrid(int width, int height, float cellWidth, float cellHeight, Vector3 origin = new Vector3())
        {
            Size = new Vector2Int(width, height);
            CellSize = new Vector2(cellWidth, cellHeight);
            gridArray = new T[width, height];
            Origin = origin;
        }

        public T GetValue(int x, int y)
        {
            if (x < 0 ||
                x >= Size.x ||
                y < 0 ||
                y >= Size.y)
            {
                return default; //Since we don't know if the value is a value type or reference type, we use the "default" keyword
            }

            return gridArray[x, y];
        }

        public void SetValue(int x, int y, T value)
        {
            if (x < 0 ||
                x >= Size.x ||
                y < 0 ||
                y >= Size.y)
            {
                return;
            }

            gridArray[x, y] = value;
            gridValueChanged?.Invoke(this, new GridValueChangedEventArgs() {
                x = x,
                y = y
            });
        }

        public Vector3 GetWorldCellPosition(int x, int y)
        {
            return new Vector3(x * CellSize.x, y * CellSize.y) + Origin;
        }

        public Vector3 GetWorldCenterPosition(int x, int y)
        {
            return GetWorldCellPosition(x, y) + ((Vector3)CellSize / 2f);
        }

        public Vector2Int GetGridPosFromWorldPos(Vector3 worldPos)
        {
            Vector2Int gridPos = new Vector2Int() {
                x = Mathf.FloorToInt((worldPos - Origin).x / CellSize.x),
                y = Mathf.FloorToInt((worldPos - Origin).y / CellSize.y)
            };

            return gridPos;
        }

        public T GetValue(Vector3 worldPos)
        {
            Vector2Int gridPos = GetGridPosFromWorldPos(worldPos);
            return GetValue(gridPos.x, gridPos.y);
        }

        public void SetValue(Vector3 worldPos, T value)
        {
            Vector2Int gridPos = GetGridPosFromWorldPos(worldPos);
            SetValue(gridPos.x, gridPos.y, value);
        }

        public void DrawGrid()
        {
            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    Vector2 bottomLeft = GetWorldCellPosition(i, j);
                    Vector2 topRight = GetWorldCellPosition(i, j) + (Vector3)(Vector2.one * CellSize);

                    Debug.DrawLine(bottomLeft, new Vector3(bottomLeft.x, topRight.y));
                    Debug.DrawLine(bottomLeft, new Vector3(topRight.x, bottomLeft.y));
                    Debug.DrawLine(new Vector3(bottomLeft.x, topRight.y), topRight);
                    Debug.DrawLine(new Vector3(topRight.x, bottomLeft.y), topRight);
                }
            }
        }
    }
}