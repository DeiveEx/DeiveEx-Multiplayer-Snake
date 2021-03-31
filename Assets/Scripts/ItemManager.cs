
using SnakeGame;
using SnakeGame.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemCell[] possibleItens;

    public void CreateNewItem(GridManager gridManager)
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

        gridManager.SetValue(itemPosition.x, itemPosition.y, itemObject);
    }
}
