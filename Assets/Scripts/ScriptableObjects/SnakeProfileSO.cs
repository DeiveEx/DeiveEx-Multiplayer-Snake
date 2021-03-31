using SnakeGame.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    [CreateAssetMenu(menuName = "SnakeGame/SnakeProfile")]
    public class SnakeProfileSO : ScriptableObject
    {
        public SnakeCell[] startBody;
    }
}