using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gautlets : Item
{
    #region Constructors
    public Gautlets(string name, int id, int output, string desc, int value, int stack, int amount)
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
