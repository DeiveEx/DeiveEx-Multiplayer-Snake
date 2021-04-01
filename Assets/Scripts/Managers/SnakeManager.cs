using SnakeGame.Grid;
using SnakeGame.InputModes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeManager : MonoBehaviour
    {
        [System.Serializable]
        private class SnakeInfo
        {
            public SnakeController controller;
            public SnakeCell[] startBody;
        }

        [SerializeField] private GridManager gridManager;

        private List<SnakeInfo> snakes = new List<SnakeInfo>();

        public void ConfigureNewSnake(SnakeController controller, Vector2Int startPosition, SnakeCell[] startBody, bool canRespawn = false)
        {
            controller.transform.SetParent(transform);
            controller.gridManager = gridManager;

            for (int i = 0; i < startBody.Length; i++)
            {
                SnakeCell segment = Instantiate(startBody[i]);

                if (i == 0)
                {
                    gridManager.SetValue(startPosition.x, startPosition.y, segment);
                    segment.gridPosition = startPosition;
                    segment.Direction = Vector2Int.up;
                }

                controller.AddSegment(segment);
            }

            if (canRespawn)
            {
                controller.died += Respawn;
            }

            snakes.Add(new SnakeInfo() {
                controller = controller,
                startBody = startBody
            });
        }

        private void Respawn(object sender, System.EventArgs e)
        {
            SnakeController controller = (SnakeController)sender;
            SnakeInfo playerInfo = snakes.First(x => x.controller == controller);

            ConfigureNewSnake(
                controller,
                gridManager.GetRandomPosition(playerInfo.startBody.Length + 2),
                playerInfo.startBody
                );
        }

        public void StartMovingSnakes()
        {
            for (int i = 0; i < snakes.Count; i++)
            {
                snakes[i].controller.StartMoving();
            }
        }

        public SnakeController[] GetPlayers()
        {
            return snakes.Select(x => x.controller).ToArray();
        }

        public int GetPlayerCount()
        {
            return snakes.Count;
        }

        public void DestroyPlayer(SnakeController player)
        {
            for (int i = 0; i < snakes.Count; i++)
            {
                if (snakes[i].controller == player)
                {
                    Destroy(snakes[i].controller.gameObject);
                    snakes.Remove(snakes[i]);
                    break;
                }
            }
        }
    }
}