
using SnakeGame;
using SnakeGame.Grid;
using SnakeGame.InputModes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private GridManager gridManager;
        [SerializeField] private SnakeManager snakeManager;
        [SerializeField] private ItemCell[] possibleItens;

        private List<ItemCell> itens = new List<ItemCell>();

        public ItemCell CreateNewItem()
        {
            ItemCell item = possibleItens[Random.Range(0, possibleItens.Length)];

            Vector2Int itemPosition = new Vector2Int();
            int tries = 0;

            while (tries < 100)
            {
                itemPosition.x = Random.Range(0, gridManager.GetGridSize().x - 1);
                itemPosition.y = Random.Range(0, gridManager.GetGridSize().y - 1);

                if (gridManager.GetValue(itemPosition.x, itemPosition.y) == null)
                    break;

                tries++;
            }

            ItemCell itemObject = Instantiate(item);
            itemObject.transform.SetParent(transform);
            itemObject.cellDestroyed += ItemObject_cellDestroyed;

            gridManager.SetValue(itemPosition.x, itemPosition.y, itemObject);
            itens.Add(itemObject);

            return itemObject;
        }

        private void ItemObject_cellDestroyed(object sender, System.EventArgs e)
        {
            Debug.Log("Removing");
            itens.Remove(sender as ItemCell);
            CreateNewItem();
        }

        public List<ItemCell> GetItens()
        {
            return itens;
        }
    }
}
