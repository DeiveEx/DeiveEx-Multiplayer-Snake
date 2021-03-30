using SnakeGame.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeManager : MonoBehaviour
    {
        [SerializeField] private SnakeController playerPrefab;
        [SerializeField] private SnakeCell[] possibleSegments;

        private List<SnakeController> players = new List<SnakeController>();

        public void CreateNewSnake(Vector2Int startPosition, GridManager gridManager, string leftArrow, string rightArrow)
        {
            SnakeController player = Instantiate(playerPrefab);
            player.gridManager = gridManager;

            //TODO change to use a "profile"
            for (int i = 0; i < 5; i++)
            {
                SnakeCell segment = Instantiate(possibleSegments[0]);

                if (i == 0)
                {
                    gridManager.SetValue(startPosition.x, startPosition.y, segment);
                    segment.gridPosition = startPosition;
                    segment.direction = Vector2Int.up;
                    segment.transform.up = (Vector2)segment.direction;
                }

                player.AddSegment(segment);
            }

            player.died += PlayerDiedHandler;
            player.SetArrows(leftArrow, rightArrow);

            players.Add(player);
        }

        private void PlayerDiedHandler(object sender, System.EventArgs e)
        {
            players.Remove((SnakeController)sender); //TODO respawn
        }
    }
}