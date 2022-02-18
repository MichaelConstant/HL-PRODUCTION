using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public Sprite selectedImage;
    //public int itemHeld;//How many items are there
    [TextArea]//a text more than one line
    public string itemInfo;

    public bool equip;
}