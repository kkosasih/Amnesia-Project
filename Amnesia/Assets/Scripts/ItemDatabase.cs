using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("vest", 0, "A basic vest", Item.ItemType.Tunic));
        items.Add(new Item("Short Sword", 1, "A tiny sword", Item.ItemType.Equipable));
    }
}
