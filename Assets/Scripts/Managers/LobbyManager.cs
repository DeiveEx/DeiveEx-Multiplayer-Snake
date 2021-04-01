using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Grid;
using SnakeGame.InputModes;
using System;

namespace SnakeGame
{
    public class LobbyManager : MonoBehaviour
    {
        [SerializeField] private SnakeManager snakeManager;
        [SerializeField] private GridManager gridManager;
        [SerializeField] private ItemManager itemManager;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private SnakeController playerPrefab;
        [SerializeField] private SnakeController aiPrefab;
        [SerializeField] private SnakeProfileSO[] availableSnakeProfiles;

        private List<string> pressedKeys = new List<string>();
        private float timeHeld;
        private int selectedSnakeProfileID;
        private List<string> availableKeys = new List<string>();
        private Vector2Int lastPlayerPosition;
        private SnakeController lastCreatedPlayer;

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
                    SnakeProfileSO selectedProfile = availableSnakeProfiles[selectedSnakeProfileID];
                    lastPlayerPosition = gridManager.GetRandomPosition(selectedProfile.startBody.Length + 2);
                    CreateNewPlayer(lastPlayerPosition);
                }
            }

            if (pressedKeys.Count == 1 && Input.GetKeyUp(pressedKeys[0]))
            {
                ResetPlayerCreation();
            }
        }

        public void UpdateCurrentPlayerProfile()
        {
            if (pressedKeys.Count == 2)
            {
                if (Input.GetKeyUp(pressedKeys[0]) || Input.GetKeyUp(pressedKeys[1]))
                {
                    CreateAiObject(lastCreatedPlayer);
                    ResetPlayerCreation();
                }
                else
                {
                    timeHeld += Time.deltaTime;

                    if (timeHeld > 1)
                    {
                        selectedSnakeProfileID = selectedSnakeProfileID + 1 >= availableSnakeProfiles.Length ? 0 : selectedSnakeProfileID + 1;
                        CreateNewPlayer(lastPlayerPosition);
                    }
                }
            }
        }

        private void CreateNewPlayer(Vector2Int startPosition)
        {
            timeHeld = 0;

            if (lastCreatedPlayer != null)
                snakeManager.DestroyPlayer(lastCreatedPlayer);

            SnakeProfileSO selectedProfile = availableSnakeProfiles[selectedSnakeProfileID];
            lastCreatedPlayer = CreatePlayerObject(startPosition, selectedProfile);
        }

        private SnakeController CreatePlayerObject(Vector2Int startPosition, SnakeProfileSO profile)
        {
            SnakeController player = Instantiate(playerPrefab);

            KeyboardInput input = player.GetComponent<KeyboardInput>();
            input.SetArrows(pressedKeys[0], pressedKeys[1]);

            snakeManager.ConfigureNewSnake(
                player,
                startPosition,
                profile.startBody,
                true //TODO remove this
                );

            return player;
        }

        private void CreateAiObject(SnakeController pairController)
        {
            SnakeController ai = Instantiate(aiPrefab);

            AiInput input = ai.GetComponent<AiInput>();
            input.SetPairController(pairController);
            input.itemManager = itemManager;

            SnakeProfileSO selectedProfile = availableSnakeProfiles[selectedSnakeProfileID];
            Vector2Int startPosition = gridManager.GetRandomPosition(selectedProfile.startBody.Length + 2);

            snakeManager.ConfigureNewSnake(
                ai,
                startPosition,
                selectedProfile.startBody,
                true
                );

            CreateItemObject(input);
        }

        private void CreateItemObject(AiInput aiController)
        {
            ItemCell item = itemManager.CreateNewItem();
            aiController.SetTarget(item);
        }

        private void ResetPlayerCreation()
        {
            selectedSnakeProfileID = 0;
            timeHeld = 0;
            pressedKeys.Clear();
            lastCreatedPlayer = null;
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
