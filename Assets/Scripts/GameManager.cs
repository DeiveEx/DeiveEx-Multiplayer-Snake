using SnakeGame.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SnakeGame
{
    public class GameManager : MonoBehaviour
    {
        public GridManager gridManager;
        public SnakeManager snakeManager;
        public ItemManager itemManager;

        private void Start()
        {
            gridManager.PrepareGrid();

            Vector2Int startPos = gridManager.GetGridSize() / 2;
            snakeManager.CreateNewSnake(startPos, gridManager, "a", "d");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                itemManager.CreateNewItem(gridManager);
            }
        }
    }
}