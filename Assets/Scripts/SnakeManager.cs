using SnakeGame.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeManager : MonoBehaviour
    {
        [SerializeField] private SnakeController playerPrefab;

        private Dictionary<SnakeController, List<SnakeCell>> players = new Dictionary<SnakeController, List<SnakeCell>>();
        private GridManager gridManager;

        public void Initialize(GridManager gridManager)
        {
            this.gridManager = gridManager;
        }

        public void CreateNewSnake(string leftArrow, string rightArrow, List<SnakeCell> startBody)
        {
            SnakeController player = Instantiate(playerPrefab);
            player.transform.SetParent(transform);
            player.gridManager = gridManager;

            Vector2Int startPosition = GetStartPosition();

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

            players.Add(player, startBody);
        }

        private Vector2Int GetStartPosition()
        {
            return gridManager.GetGridSize() / 2;
        }

        private void Player_died(object sender, EventArgs e)
        {
            SnakeController controller = (SnakeController)sender;
            List<SnakeCell> startBody = players[controller];
            string leftArrow = controller.leftArrow;
            string rightArrow = controller.rightArrow;

            players.Remove(controller);
            Destroy(controller.gameObject);

            CreateNewSnake(leftArrow, rightArrow, startBody);
        }
    }
}