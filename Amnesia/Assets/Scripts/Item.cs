using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public string itemName;
    public int itemId;
    public string itemDesc;
    public Texture2D itemIcon;
    public ItemType itemtype;

    public enum ItemType
    {
        Tool,
        Consumable,
        Quest
    }

    public Item(string name, int id, string desc, ItemType type)
    {
        itemName = name;
        itemId = id;
        itemDesc = desc;
        itemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
        itemtype = type;
    }

    public Item()
    {

    }
}

