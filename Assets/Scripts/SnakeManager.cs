using SnakeGame.Grid;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeManager : MonoBehaviour
    {
        private class SnakeInfo
        {
            public SnakeController controller;
            public List<SnakeCell> startBody;
        }

        [SerializeField] private SnakeController playerPrefab;
        [SerializeField] private GridManager gridManager;

        private List<SnakeInfo> players = new List<SnakeInfo>();
        private bool gameStarted;

        public void CreateNewSnake(string leftArrow, string rightArrow, List<SnakeCell> startBody)
        {
            SnakeController player = Instantiate(playerPrefab);
            player.transform.SetParent(transform);
            player.gridManager = gridManager;

            Vector2Int startPosition = GetStartPosition(startBody.Count);

            for (int i = 0; i < startBody.Count; i++)
            {
                SnakeCell segment = Instantiate(startBody[i]);

                if (i == 0)
                {
                    gridManager.SetValue(startPosition.x, startPosition.y, segment);
                    segment.gridPosition = startPosition;
                    segment.direction = Vector2Int.up;
                    segment.transform.up = (Vector2)segment.direction;
                }

                player.AddSegment(segment);
            }

            player.died += Player_died;
            player.SetArrows(leftArrow, rightArrow);

            players.Add(new SnakeInfo() {
                controller = player,
                startBody = startBody
            });
        }

        private Vector2Int GetStartPosition(int margin)
        {
            return new Vector2Int() {
                x = Random.Range(margin, gridManager.GetGridSize().x - margin),
                y = Random.Range(margin, gridManager.GetGridSize().y - margin),
            };
        }

        private void Player_died(object sender, System.EventArgs e)
        {
            SnakeController oldController = (SnakeController)sender;
            SnakeInfo playerInfo = players.First(x => x.controller == oldController);
            string leftArrow = oldController.leftArrow;
            string rightArrow = oldController.rightArrow;

            players.Remove(playerInfo);
            Destroy(oldController.gameObject);

            CreateNewSnake(leftArrow, rightArrow, playerInfo.startBody);
            players[players.Count - 1].controller.StartMoving();
        }

        public void StartMovingSnakes()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].controller.StartMoving();
            }
        }
    }
}