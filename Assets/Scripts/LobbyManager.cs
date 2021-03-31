using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Grid;

namespace SnakeGame
{
    public class LobbyManager : MonoBehaviour
    {
        public SnakeManager snakeManager;
        public GridManager gridManager;
        public GameObject tutorialPanel;
        public SnakeProfileSO[] availableSnakeProfiles;

        private List<string> pressedKeys = new List<string>();
        private float timeHeld;
        private int selectedSnakeProfileID;
        private List<string> availableKeys = new List<string>();
        private Vector2Int lastPlayerPosition;

        private const string POSSIBLE_KEYS = "0123456789abcdefghijklmnopqrstuvwxyz";

        private void Awake()
        {
            for (int i = 0; i < POSSIBLE_KEYS.Length; i++)
            {
                availableKeys.Add(POSSIBLE_KEYS[i].ToString());
            }
        }

        public void CheckForNewPlayers()
        {
            if (Input.anyKeyDown)
            {
                CheckForValidKeys(Input.inputString);

                if (pressedKeys.Count == 2)
                {
                    CreateNewPlayer();
                }
            }

            if (pressedKeys.Count == 1 && Input.GetKeyUp(pressedKeys[0]))
            {
                ResetPlayerCreation();
            }

            if (pressedKeys.Count == 2)
            {
                if (Input.GetKeyUp(pressedKeys[0]) || Input.GetKeyUp(pressedKeys[1]))
                {
                    ResetPlayerCreation();
                }
                else
                {
                    timeHeld += Time.deltaTime;

                    if (timeHeld > 1)
                    {
                        CycleProfile();
                    }
                }
            }
        }

        private void CreateNewPlayer()
        {
            SnakeProfileSO selectedProfile = availableSnakeProfiles[selectedSnakeProfileID];
            lastPlayerPosition = gridManager.GetRandomPosition(selectedProfile.startBody.Length);
            CreatePlayerObject(lastPlayerPosition);
        }

        private void CycleProfile()
        {
            timeHeld = 0;

            SnakeController[] createdPlayers = snakeManager.GetPlayers();
            SnakeController lastCreatedPlayer = createdPlayers[createdPlayers.Length - 1];
            snakeManager.DestroyPlayer(lastCreatedPlayer);

            selectedSnakeProfileID = selectedSnakeProfileID + 1 >= availableSnakeProfiles.Length ? 0 : selectedSnakeProfileID + 1;

            CreatePlayerObject(lastPlayerPosition);
        }

        private void CreatePlayerObject(Vector2Int startPosition)
        {
            SnakeProfileSO selectedProfile = availableSnakeProfiles[selectedSnakeProfileID];
            snakeManager.CreateNewSnake(
                pressedKeys[0],
                pressedKeys[1],
                startPosition,
                selectedProfile.startBody
                );
        }

        private void ResetPlayerCreation()
        {
            selectedSnakeProfileID = 0;
            pressedKeys.Clear();
        }

        private void CheckForValidKeys(string key)
        {
            if (!string.IsNullOrEmpty(key) && availableKeys.Contains(key) && pressedKeys.Count < 2)
            {
                pressedKeys.Add(Input.inputString);
            }
        }

        public void ShowTutorial()
        {
            tutorialPanel.SetActive(true);
        }

        public void HideTutorial()
        {
            tutorialPanel.SetActive(false);
        }
    }
}
