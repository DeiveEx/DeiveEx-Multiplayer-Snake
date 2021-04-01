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
                lobbyManager.UpdateCurrentPlayerProfile();

                if (Input.GetKeyDown(KeyCode.Return) && snakeManager.GetPlayerCount() > 0)
                {
                    gameStarted = true;
                    snakeManager.StartMovingSnakes();
                    lobbyManager.HideTutorial();
                }
            }
        }
    }
}