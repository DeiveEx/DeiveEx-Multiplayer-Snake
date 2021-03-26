using SnakeGame.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] private float speed;
        public string leftArrow;
        public string rightArrow;

        private GenericGrid<GridCell> grid;
        private List<SnakeCell> bodySegments = new List<SnakeCell>();
        private float moveCounter;
        private SnakeCell head;

        public void SetStartingBody(Vector2Int startPosition, GenericGrid<GridCell> grid, List<SnakeCell> startBodySegments)
        {
            this.grid = grid;

            for (int i = 0; i < startBodySegments.Count; i++)
            {
                SnakeCell segment = startBodySegments[i];
                segment.gridPosition = startPosition + (segment.GetDirection() * i);
                grid.SetValue(segment.gridPosition.x, segment.gridPosition.y, segment);
                bodySegments.Add(segment);
                head = segment;
            }
        }

        public void AddSegment(SnakeCell segment)
        {
            if (bodySegments.Count != 0)
            {
                //TODO
            }

            bodySegments.Add(segment);
        }

        private void Update()
        {
            if (Input.GetKeyDown(leftArrow))
            {
                head.Rotate(1);
            }
            else if (Input.GetKeyDown(rightArrow))
            {
                head.Rotate(-1);
            }

            CheckIfCanMove();
        }

        private void CheckIfCanMove()
        {
            moveCounter += Time.deltaTime;

            if (moveCounter > 1 / speed)
            {
                moveCounter = 0;
                UpdateBody();
            }
        }

        private void UpdateBody()
        {
            for (int i = 0; i < bodySegments.Count; i++)
            {
                SnakeCell segment = bodySegments[i];
                Vector2Int newPosition = segment.gridPosition + segment.GetDirection();
                grid.SetValue(newPosition.x, newPosition.y, segment);
                grid.SetValue(segment.gridPosition.x, segment.gridPosition.y, null);
                segment.gridPosition = newPosition;
            }
        }
    }
}
