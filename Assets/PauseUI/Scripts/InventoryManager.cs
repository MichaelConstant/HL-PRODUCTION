using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory myBag;
    public GameObject slotGrid;//drag "Grid" into it
    public Slot slotPrefab;
    public Text itemInformation;
    public Text itemName;

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }
    private void Awake()
    {
        if (instance != null)//singleton this Manager
            Destroy(this);
        instance = this;
    }
    public static void UpdateItemInfo(string itemDestcription,string itemName) //update the info of item in "itemInformation"
    {
        instance.itemInformation.text = itemDestcription;
        instance.itemName.text = itemName;
    }


    public static void CreateNewItem(item item)//deliver all information of item to <Slot>,instantiate a <Slot>as child of "Grid"
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.transform.localScale = new Vector3(1, 1, 1);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;

        SpriteState state = new SpriteState();
        state.selectedSprite = item.selectedImage;
        newItem.button.spriteState = state;
    }
    public static void RefreshItem()
    {
        if (instance != null)
        {
            for (int i = 0; i < instance.slotGrid.transform.childCount; i++)//delete all children of "Grid"
            {
                if (instance.slotGrid.transform.childCount == 0)
                    break;
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < instance.myBag.itemList.Count; i++)  //Re-locate the Scriptable Objects of class <item> in Scriptable Object of class<Inventory>"myBag"and put them in the "Grid"
            {
                CreateNewItem(instance.myBag.itemList[i]);
            }
        }
    }
}
