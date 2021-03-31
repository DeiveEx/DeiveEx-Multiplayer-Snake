using SnakeGame.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SnakeGame
{
    public class GameManager : MonoBehaviour
    {
        public GridManager gridManager;
        public ItemManager itemManager;
        public LobbyManager lobbyManager;
        public SnakeManager snakeManager;

        private bool gameStarted;

        private void Start()
        {
            gridManager.GenerateWalls();
            lobbyManager.ShowTutorial();
        }

        private void Update()
        {
            if (!gameStarted)
            {
                lobbyManager.CheckForNewPlayers();

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    gameStarted = true;
                    snakeManager.StartMovingSnakes();
                    lobbyManager.HideTutorial();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    itemManager.CreateNewItem(gridManager);
                }
            }
        }
    }
}