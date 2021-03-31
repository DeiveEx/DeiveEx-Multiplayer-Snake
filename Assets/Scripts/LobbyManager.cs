using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Grid;

namespace SnakeGame
{
    public class LobbyManager : MonoBehaviour
    {
        public SnakeManager snakeManager;
        public SnakeCell snakeCell; //TODO remove this

        private const string POSSIBLE_KEYS = "0123456789qwertyuiopasdfghjklzxcvbnm";
        private List<string> pressedKeys = new List<string>();

        public void CheckForNewPlayers()
        {
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
