using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class WallCell : GridCell
    {
        public override void OnCollision(SnakeCell otherCell)
        {
            otherCell.DestroyCell();
        }
    }
}