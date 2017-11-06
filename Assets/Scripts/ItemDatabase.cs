using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start()
    {
        //Got bored started making random items
        items.Add(new Item("vest", 0, 2, "A cloth vest made of cloth", 1, 50, Item.ItemType.Tunic));
        items.Add(new Item("Short Sword", 1, 4, "A teeny tiny sword named sword", 1, 100, Item.ItemType.Equipable));
        items.Add(new Item("Apple", 2, 2, "Unusually red fruit \"It's red like your Health bar\"- Unknown Man #1", 5, 10, Item.ItemType.Consumable));
        items.Add(new Item("Garbage", 3, 999, "It's garbage \"One man's trash...\" - Older Unknown Man", 999, 1, Item.ItemType.Quest));
        items.Add(new Item("THE ULTIMATE BAG", 4, 24, "\"It a bag that unlocks all your inventory slots\" -Developer #4", 500, 1, Item.ItemType.Pouch));
        items.Add(new Item("Neke Air Jordan's", 5, 1, "\"It's in the game\" - Spokesperson", 900, 1, Item.ItemType.Boots));
        items.Add(new Item("Oven Mitt", 6, 7, "It's one oven mitt \"Your gloveless hand feels cold\"", 50, 2, Item.ItemType.Gautlets));
        items.Add(new Item("Stained Pot", 7, 255, "Label on side: \"Now made with stainless steel!!\"", 20, 1, Item.ItemType.Helmet));
    }
}
