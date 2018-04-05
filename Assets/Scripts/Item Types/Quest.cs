using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest : Item {

    #region Constructors
    public Quest(string name, int id, int output, string desc, int value, int stack, int amount)
    {
        itemName = name;
        itemId = id;
        itemOutput = output;
        itemDesc = desc;
        itemStack = stack;
        itemStackAmount = amount;
        itemValue = value;
        itemIcon = Resources.Load<Sprite>("Item Icons/" + name);
    }
    #endregion
}
