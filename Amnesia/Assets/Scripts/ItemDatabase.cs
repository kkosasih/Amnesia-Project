using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start()
    {
        //Got bored started making random items
        items.Add(new Item("vest", 0, 2, "A cloth vest made of cloth", 1, Item.ItemType.Tunic));
        items.Add(new Item("Short Sword", 1, 4, "A teeny tiny sword named sword", 1, Item.ItemType.Equipable));
        items.Add(new Item("Apple", 2, 2, "Unusually red fruit \"It's red like your Health bar\"- Unknown Man #1", 5, Item.ItemType.Consumable));
        items.Add(new Item("Garbage", 3, 999, "It's garbage \"One man's trash...\" - Older Unknown Man", 999, Item.ItemType.Quest));
    }
}
