using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SnakeGame.Grid;
using SnakeGame;

namespace Tests
{
    [TestFixture]
    public class SnakeCellTests
    {
        private GridManager gridManager;

        private GameObject obj;
        private SnakeCell cell;

        [SetUp]
        public void Initialize()
        {
            obj = new GameObject();

            gridManager = obj.AddComponent<GridManager>();

            cell = obj.AddComponent<SnakeCell>();
            cell.direction = Vector2Int.up;
            cell.transform.up = (Vector2)cell.direction;
        }

        [UnityTest]
        public IEnumerator MoveForward_InitialPosIs00DirIsUp_EndPosIs01()
        {
            yield return null;
            cell.MoveForward(gridManager);
            Assert.AreEqual(Vector2Int.up, cell.gridPosition);
        }

        [UnityTest]
        public IEnumerator MoveForward_InitialPosIs00DirIsUp_EndPosIsNot00()
        {
            yield return null;
            cell.MoveForward(gridManager);
            Assert.AreNotEqual(Vector2Int.zero, cell.gridPosition);
        }

        [UnityTest]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(100, ExpectedResult = null)]
        public IEnumerator Rotate_DirectionIsRightRotatePositive_DirectionNotEqualsLeft(int direction)
        {
            cell.direction = Vector2Int.right;
            cell.transform.up = (Vector2)cell.direction;
            cell.SetRotationDirection(direction);
            cell.UpdateDirection();
            yield return null;
            Assert.AreNotEqual(Vector2Int.left, cell.direction);
        }

        [UnityTest]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(100, ExpectedResult = null)]
        public IEnumerator Rotate_DirectionIsUpRotatePositive_DirectionEqualsLeft(int direction)
        {
            cell.SetRotationDirection(direction);
            cell.UpdateDirection();
            yield return null;
            Assert.AreEqual(Vector2Int.left, cell.direction);
        }

        [UnityTest]
        [TestCase(-1, ExpectedResult = null)]
        [TestCase(-100, ExpectedResult = null)]
        public IEnumerator Rotate_DirectionIsUpRotateNegative_DirectionEqualsRight(int direction)
        {
            cell.SetRotationDirection(direction);
            cell.UpdateDirection();
            yield return null;
            Assert.AreEqual(Vector2Int.right, cell.direction);
        }
    }
}
