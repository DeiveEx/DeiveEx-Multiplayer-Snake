using SnakeGame.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.InputModes
{
    [RequireComponent(typeof(SnakeController))]
    public class AiInput : MonoBehaviour
    {
        [HideInInspector] public ItemManager itemManager;

        private SnakeController controller;
        private SnakeController pair;
        private ItemCell target;

        private void Awake()
        {
            controller = GetComponent<SnakeController>();

            controller.moving += Controller_moving;
        }

        private void Controller_moving(object sender, System.EventArgs e)
        {
            //Simple AI
            SnakeCell head = controller.GetHead();
            Vector2Int position = head.gridPosition;
            Vector2Int originalDir = head.Direction;
            int dir = 0;
            float closestDistance = float.MaxValue;

            for (int i = -1; i <= 1; i++)
            {
                head.SetRotationDirection(i);
                head.UpdateDirection();
                float distance = Vector2.Distance(target.gridPosition, position + head.Direction);

                if (distance < closestDistance)
                {
                    dir = i;
                    closestDistance = distance;
                }

                head.Direction = originalDir;
            }

            controller.SetDirection(dir);
        }

        public void SetPairController(SnakeController pair)
        {
            this.pair = pair;
        }

        public void SetTarget(ItemCell item)
        {
            this.target = item;
            item.cellDestroyed += Item_cellDestroyed;
        }

        private void Item_cellDestroyed(object sender, System.EventArgs e)
        {
            Debug.Log("getting new item");

            var itens = itemManager.GetItens();
            SetTarget(itens[itens.Count - 1]);
        }
    }
}
