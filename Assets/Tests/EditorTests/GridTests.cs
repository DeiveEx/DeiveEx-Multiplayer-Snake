using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SnakeGame.Grid;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class GridTests
    {
        GenericGrid<GridCell> grid;

        [SetUp]
        public void Initialize()
        {
            grid = new GenericGrid<GridCell>(100, 100, 1, 1);
        }

        [Test]
        public void SetValue_Set0x0_IsNotNull()
        {
            grid.SetValue(0, 0, new GridCell());
            Assert.IsNotNull(grid.GetValue(0, 0));
        }

        [Test]
        public void SetValue_Set101x101_IsNull()
        {
            grid.SetValue(101, 101, new GridCell());
            Assert.IsNull(grid.GetValue(101, 101));
        }

        [Test]
        public void SetValue_SetMinusOneZero_IsNull()
        {
            grid.SetValue(-1, 0, new GridCell());
            Assert.IsNull(grid.GetValue(-1, 0));
        }
    }
}
