using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame.Grid
{
    public class ItemCell : GridCell
    {
        public float slowDownValue;
        public SnakeCell segmentToAdd;

        public override void OnCollision(GridCell otherCell)
        {

            DestroyCell();
        }
    }
}