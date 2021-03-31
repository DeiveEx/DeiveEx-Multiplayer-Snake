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
        public SnakeManager snakeManager;
        public ItemManager itemManager;
        public SnakeCell snakeCell; //TODO remove this

        private bool gameStarted;
        private const string POSSIBLE_KEYS = "0123456789qwertyuiopasdfghjklzxcvbnm";
        private List<string> pressedKeys = new List<string>();
        private float pressedTime = 0;

        private void Start()
        {
            gridManager.PrepareGrid();
        }

        private void Update()
        {
            if (gameStarted)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    itemManager.CreateNewItem(gridManager);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    gameStarted = true;
                    snakeManager.StartMovingSnakes();
                }

                if (Input.anyKeyDown)
                {
                    CheckForValidKeys(Input.inputString);
                }

                if (pressedKeys.Count == 1 && Input.GetKeyUp(pressedKeys[0]))
                {
                    pressedKeys.Remove(pressedKeys[0]);
                }

                if (pressedKeys.Count == 2 && (Input.GetKeyUp(pressedKeys[0]) || Input.GetKeyUp(pressedKeys[1])))
                {
                    CreatePlayer();
                }
            }
        }

        private void CreatePlayer()
        {
            //TODO change to use a "profile"
            snakeManager.CreateNewSnake(pressedKeys[0], pressedKeys[1], new List<SnakeCell>() {
                snakeCell,
                snakeCell,
                snakeCell
            });

            pressedKeys.Clear();
        }

        private void CheckForValidKeys(string key)
        {
            if (!string.IsNullOrEmpty(key) && POSSIBLE_KEYS.Contains(key) && pressedKeys.Count < 2)
            {
                pressedKeys.Add(Input.inputString);
            }
        }
    }
}