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
            gridManager.GenerateGrid(100, 100, 1, 1, Vector3.zero);

            cell = obj.AddComponent<SnakeCell>();
            cell.Direction = Vector2Int.up;
            cell.gridPosition = Vector2Int.zero;
            gridManager.SetValue(cell.gridPosition.x, cell.gridPosition.y, cell);
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
            yield return null;
            cell.Direction = Vector2Int.right;
            cell.SetRotationDirection(direction);
            cell.UpdateDirection();
            yield return null;
            Assert.AreNotEqual(Vector2Int.left, cell.Direction);
        }

        [UnityTest]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(100, ExpectedResult = null)]
        public IEnumerator Rotate_DirectionIsUpRotatePositive_DirectionEqualsLeft(int direction)
        {
            yield return null;
            cell.SetRotationDirection(direction);
            cell.UpdateDirection();
            yield return null;
            Assert.AreEqual(Vector2Int.left, cell.Direction);
        }

        [UnityTest]
        [TestCase(-1, ExpectedResult = null)]
        [TestCase(-100, ExpectedResult = null)]
        public IEnumerator Rotate_DirectionIsUpRotateNegative_DirectionEqualsRight(int direction)
        {
            yield return null;
            cell.SetRotationDirection(direction);
            cell.UpdateDirection();
            yield return null;
            Assert.AreEqual(Vector2Int.right, cell.Direction);
        }

        [UnityTest]
        public IEnumerator CheckCollision_Wall1CellAboveSnakeCell_CollisionIsTrue()
        {
            yield return null;
            GameObject wallObject = new GameObject();
            WallCell wall = wallObject.AddComponent<WallCell>();
            gridManager.SetValue(0, 1, wall);
            Assert.IsTrue(cell.CheckCollision(gridManager));
        }

        [UnityTest]
        public IEnumerator CheckCollision_Wall2CellsAboveSnakeCell_CollisionIsFalse()
        {
            yield return null;
            GameObject wallObject = new GameObject();
            WallCell wall = wallObject.AddComponent<WallCell>();
            gridManager.SetValue(0, 2, wall);
            Assert.IsFalse(cell.CheckCollision(gridManager));
        }
    }
}
