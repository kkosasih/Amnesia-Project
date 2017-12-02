using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    //Sidenote: When putting items on the floor to be pickupable (with x) just put on ItemLayer and make object name the one you want added
    public string itemName;             //The name
    public int itemId;                  //Callable id
    public int itemOutput;              //The Defense, Damage, or Health Regain strength of the item
    public string itemDesc;             //The Description
    public int itemStack;               //Max stack amount of an item
    public int itemStackAmount;
    public int itemValue;               //The monatery value of the item
    public Texture2D itemIcon;          //How it looks

    /*public enum ItemType
    {
        Equipable,                      //Tools
        Tunic,                          //Chest Wearable
        Boots,                          //..... Wearable
        Gautlets,                       //..............
        Pouch,                          //.....ODC......
        Helmet,                         //...REASONS....
        Consumable,                     //..............
        Quest                           //Non-Discardable, Sellable, or Equipable items
    }*/

    public Item(string name, int id, int output, string desc, int value, int stack, int amount)
    {
        itemName = name;
        itemId = id;
        itemOutput = output;

        itemDesc = desc;
        itemStack = stack;
        itemStackAmount = stack;
        itemValue = value;
        itemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
    }

    public Item()
    {

    }
}