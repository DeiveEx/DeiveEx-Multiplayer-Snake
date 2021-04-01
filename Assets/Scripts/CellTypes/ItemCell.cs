using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class ItemCell : GridCell
    {
        public SnakeCell segmentToAdd;

        public override void OnCollision(SnakeCell otherCell)
        {
            Debug.Log("Destroying");
            DestroyCell();
            otherCell.OnItemConsumed(Instantiate(segmentToAdd));
        }
    }
}