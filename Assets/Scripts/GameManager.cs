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

        public SnakeCell snakeCell; //TODO remove this

        private void Start()
        {
            gridManager.PrepareGrid();
            snakeManager.Initialize(gridManager);

            //TODO change to use a "profile"
            snakeManager.CreateNewSnake("a", "d", new List<SnakeCell>() {
                snakeCell,
                snakeCell,
                snakeCell
            });
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