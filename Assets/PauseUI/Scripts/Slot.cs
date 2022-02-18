using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public item slotItem;// in order to deliver "info"
    public Image slotImage;
    public Button button;

    public void ItemOnClicked()//when click a slot , the info of the item delivered to it is showned in "itemInformation"
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo, slotItem.itemName);
    }
    
}
