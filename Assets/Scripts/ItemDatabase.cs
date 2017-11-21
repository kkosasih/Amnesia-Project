using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public string tempname;
    public int var1;
    public int var2;
    public int var3;

    void Start()
    {
        //Got bored started making random items
        items.Add(new Item("vest", 0, 2, "A cloth vest made of cloth", 50, 1, 1, Item.ItemType.Tunic));
        items.Add(new Item("Short Sword", 1, 4, "A teeny tiny sword named sword", 100, 1, 1, Item.ItemType.Equipable));
        items.Add(new Item("Apple", 2, 2, "Unusually red fruit \"It's red like your Health bar\"- Unknown Man #1", 5, 10, 1, Item.ItemType.Consumable));
        items.Add(new Item("Garbage", 3, 999, "It's garbage \"One man's trash...\" - Older Unknown Man", 1, 999, 1, Item.ItemType.Quest));
        items.Add(new Item("THE ULTIMATE BAG", 4, 24, "\"It a bag that unlocks all your inventory slots\" -Developer #4", 500, 1, 1, Item.ItemType.Pouch));
        items.Add(new Item("Neke Air Jordan's", 5, 1, "\"It's in the game\" - Spokesperson", 900, 1, 1, Item.ItemType.Boots));
        items.Add(new Item("Oven Mitt", 6, 7, "It's one Lefty oven mitt \"Your gloveless hand feels cold\"", 50, 1, 1, Item.ItemType.Gautlets));
        items.Add(new Item("Stained Pot", 7, 255, "Label on side: \"Now made with stainless steel!!\"", 20, 1, 1, Item.ItemType.Helmet));
    }

    public string randomdrop(string tempname)
    {
        var1 = Random.Range(1, 100);
        var2 = Random.Range(1, 10);
        var3 = Random.Range(1, 10);

        if (var1 <= 5)                          //5%
        {
            if (tempname == "slime")
            {
                tempname = "vest";
            }
        }
        else if ((var1 > 5) && (var1 <= 15))    //10%
        {
            if (tempname == "slime")
            {
                tempname = "Short Sword";
            }
        }
        else if ((var1 > 15) && (var1 <= 35))   //20%
        {
            if (tempname == "slime")
            {
                tempname = "Apple";
            }
        }
        else if ((var1 > 35) && (var1 <= 65))   //30%
        {
            if (tempname == "slime")
            {
                tempname = "Apple";
            }
        }
        else if ((var1 > 65) && (var1 <= 85))   //20%
        {
            if (tempname == "slime")
            {
                tempname = "Apple";
            }
        }
        else if ((var1 > 85) && (var1 <= 95))   //10%
        {
            if (tempname == "slime")
            {
                tempname = "Apple";
            }
        }
        else                                    //5%
        {
            if (tempname == "slime")
            {
                tempname = "Apple";
            }
        }
        return tempname;
    }
}
