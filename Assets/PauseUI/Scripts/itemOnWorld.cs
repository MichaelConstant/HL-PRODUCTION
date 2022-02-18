using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour //This Script should be on every prop and dragged reference to each
{
    public item thisItem;//To store the Scriptable object
    public Inventory playerInventory;//To store the inventory of bag 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }
    void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
            InventoryManager.CreateNewItem(thisItem);
        }
            InventoryManager.RefreshItem();  // Refresh Slots in the bag (need change, do this whenever a prop is added or deleted 
    }
}
