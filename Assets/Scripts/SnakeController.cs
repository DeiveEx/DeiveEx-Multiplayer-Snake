using SnakeGame.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeController : MonoBehaviour
    {
        public GridManager gridManager;
        [SerializeField] private float speed;

        public event EventHandler died;

        private List<SnakeCell> bodySegments = new List<SnakeCell>();
        private float moveCounter;
        private SnakeCell head;
        private string leftArrow;
        private string rightArrow;

        public void SetArrows(string left, string right)
        {
            leftArrow = left;
            rightArrow = right;
        }

        public void AddSegment(SnakeCell segment)
        {
            //Create the new segment in front of the head
            if (head != null)
            {
                Vector2Int newPosition = head.gridPosition + head.direction;
                segment.gridPosition = newPosition;
                segment.direction = head.direction;
                gridManager.SetValue(newPosition.x, newPosition.y, segment);
                head.parentCell = segment;
                head.cellDestroyed -= HeadDestroyedHandler;
            }

            bodySegments.Add(segment);
            head = segment; //The head is always the newest segment
            head.cellDestroyed += HeadDestroyedHandler;
        }

        private void HeadDestroyedHandler(object sender, EventArgs e)
        {
            for (int i = 0; i < bodySegments.Count; i++)
            {
                if (bodySegments[i] != head)
                    bodySegments[i].DestroyCell();
            }

            died?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(leftArrow))
            {
                head.SetRotationDirection(1);
            }
            else if (Input.GetKeyDown(rightArrow))
            {
                head.SetRotationDirection(-1);
            }

            CheckIfCanMove();
        }

        private void CheckIfCanMove()
        {
            moveCounter += Time.deltaTime;

            if (moveCounter > 1 / speed)
            {
                moveCounter = 0;

                if (!head.CheckCollision(gridManager))
                {
                    UpdateBody();
                }
            }
        }

        private void UpdateBody()
        {
            //Position
            for (int i = bodySegments.Count - 1; i >= 0; i--)
            {
                bodySegments[i].MoveForward(gridManager);
            }

            //Direction
            for (int i = 0; i < bodySegments.Count - 1; i++)
            {
                bodySegments[i].direction = bodySegments[i + 1].direction;
            }
        }
    }
}
