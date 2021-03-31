using SnakeGame.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class EnginePowerSnakeCell : SnakeCell
    {
        public float speedUpValue;

        public override void ExecuteSegmentAddedEffect()
        {
            ItemEffectEventArgs itemEffectEventArgs = new ItemEffectEventArgs() {
                speedModifier = speedUpValue
            };

            OnSegmentAdded(itemEffectEventArgs);
        }
    }
}
