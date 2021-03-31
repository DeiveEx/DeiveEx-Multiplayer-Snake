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
            public SnakeCell[] startBody;
        }

        [SerializeField] private SnakeController playerPrefab;
        [SerializeField] private GridManager gridManager;

        private List<SnakeInfo> players = new List<SnakeInfo>();

        public void CreateNewSnake(string leftArrow, string rightArrow, Vector2Int startPosition, SnakeCell[] startBody)
        {
            SnakeController player = Instantiate(playerPrefab);
            player.transform.SetParent(transform);
            player.gridManager = gridManager;

            for (int i = 0; i < startBody.Length; i++)
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

        private void Player_died(object sender, System.EventArgs e)
        {
            SnakeController oldController = (SnakeController)sender;
            SnakeInfo playerInfo = players.First(x => x.controller == oldController);
            string leftArrow = oldController.leftArrow;
            string rightArrow = oldController.rightArrow;

            DestroyPlayer(oldController);

            CreateNewSnake(
                leftArrow,
                rightArrow,
                gridManager.GetRandomPosition(playerInfo.startBody.Length),
                playerInfo.startBody
                );

            players[players.Count - 1].controller.StartMoving();
        }

        public void StartMovingSnakes()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].controller.StartMoving();
            }
        }

        public SnakeController[] GetPlayers()
        {
            return players.Select(x => x.controller).ToArray();
        }

        public void DestroyPlayer(SnakeController player)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].controller == player)
                {
                    Destroy(players[i].controller.gameObject);
                    players.Remove(players[i]);
                    break;
                }
            }
        }
    }
}