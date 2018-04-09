using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase
{
    #region Attributes
    public List<Item> items = new List<Item>();
    public int var1;
    public int var2;
    public int var3;
    private string tempname;
    #endregion

    #region Constructors
    // Default constructor
    public ItemDatabase ()
    {
        items = new List<Item>();
        //Got bored started making random items
        items.Add(new Tunic("vest", 0, 2, "A cloth vest made of cloth", 50, 1, 1));
        items.Add(new Equipable("Short Sword", 1, 4, "A teeny tiny sword named sword", 100, 1, 1));
        items.Add(new Consumable("Apple", 2, 2, "Unusually red fruit \"It's red like your Health bar\"- Unknown Man #1", 5, 10, 1));
        items.Add(new Quest("Garbage", 3, 999, "It's garbage \"One man's trash...\" - Older Unknown Man", 1, 999, 1));
        items.Add(new Pouch("THE ULTIMATE BAG", 4, 24, "\"It a bag that unlocks all your inventory slots\" -Developer #4", 500, 1, 1));
        items.Add(new Boots("Neke Air Jordan's", 5, 1, "\"It's in the game\" - Spokesperson", 900, 1, 1));
        items.Add(new Gautlets("Oven Mitt", 6, 7, "It's one Lefty oven mitt \"Your gloveless hand feels cold\"", 50, 1, 1));
        items.Add(new Helmet("Stained Pot", 7, 255, "Label on side: \"Now made with stainless steel!!\"", 20, 1, 1));
        items.Add(new Quest("Wood", 8, 0, "Lumber for building.", 5, 99, 1));
    }
    #endregion

    #region Methods
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
    #endregion
}
