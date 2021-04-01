using SnakeGame.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeController : MonoBehaviour
    {
        [HideInInspector] public GridManager gridManager;
        [SerializeField] private float startSpeed = 1;
        [SerializeField] private float minSpeed = .1f;
        [SerializeField] private float speedModifierOnSegmentAdded = .1f;

        public event EventHandler died;
        public event EventHandler moving;

        private List<SnakeCell> bodySegments = new List<SnakeCell>();
        private float moveCounter;
        private SnakeCell head;
        private bool canMove;
        private float speed;

        private void Awake()
        {
            speed = startSpeed;
        }

        private void Update()
        {
            CheckIfCanMove();
        }

        public void SetDirection(int dir)
        {
            head.SetRotationDirection(dir);
        }

        public void StartMoving()
        {
            canMove = true;
        }

        public void AddSegment(SnakeCell segment)
        {
            //Create the new segment in front of the head
            if (head != null)
            {
                Vector2Int newPosition = head.gridPosition + head.Direction;
                segment.gridPosition = newPosition;
                segment.Direction = head.Direction;
                gridManager.SetValue(newPosition.x, newPosition.y, segment);
                head.parentCell = segment;
                head.cellDestroyed -= Head_cellDestroyed;
                head.itemConsumed -= Head_itemConsumed;
            }

            bodySegments.Add(segment);
            segment.transform.SetParent(transform);

            head = segment; //The head is always the newest segment
            head.cellDestroyed += Head_cellDestroyed;
            head.itemConsumed += Head_itemConsumed;

            ModifySpeed(speedModifierOnSegmentAdded);
            head.ExecuteSegmentAddedEffect(this);
        }

        public void ModifySpeed(float value)
        {
            speed += value;

            if (speed < minSpeed)
            {
                speed = minSpeed;
            }
        }

        private void Head_itemConsumed(object sender, SnakeSegmentEventArgs e)
        {
            AddSegment(e.segmentToAdd);
        }

        private void Head_cellDestroyed(object sender, EventArgs e)
        {
            Die();
        }

        public void Die()
        {
            for (int i = 0; i < bodySegments.Count; i++)
            {
                if (bodySegments[i] != head)
                {
                    bodySegments[i].DestroyCell();
                }
            }

            bodySegments.Clear();
            speed = startSpeed;
            moveCounter = 0;
            head = null;

            died?.Invoke(this, EventArgs.Empty);
        }

        private void CheckIfCanMove()
        {
            if (!canMove)
                return;

            moveCounter += Time.deltaTime;

            if (moveCounter > 1 / speed)
            {
                moving?.Invoke(this, EventArgs.Empty);
                moveCounter = 0;
                head.UpdateDirection();

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
                bodySegments[i].Direction = bodySegments[i + 1].Direction;
                bodySegments[i].transform.rotation = bodySegments[i + 1].transform.rotation;
            }
        }

        public SnakeCell GetHead()
        {
            return head;
        }
    }
}
