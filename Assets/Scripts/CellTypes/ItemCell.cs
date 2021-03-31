using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame.Grid
{
    public class ItemInfoEventArgs : EventArgs
    {
        public SnakeCell segmentToAdd;
    }

    public class ItemCell : GridCell
    {
        public SnakeCell segmentToAdd;

        public override void OnCollision(SnakeCell otherCell)
        {
            DestroyCell();
            otherCell.OnItemConsumed(Instantiate(segmentToAdd));
        }
    }
}