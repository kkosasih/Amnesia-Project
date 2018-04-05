using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boots : Item
{
    #region Constructors
    public Boots(string name, int id, int output, string desc, int value, int stack, int amount)
    {
        itemName = name;
        itemId = id;
        itemOutput = output;
        itemDesc = desc;
        itemStack = stack;
        itemStackAmount = 1;
        itemValue = value;
        itemIcon = Resources.Load<Sprite>("Item Icons/" + name);
    }
    #endregion
}
