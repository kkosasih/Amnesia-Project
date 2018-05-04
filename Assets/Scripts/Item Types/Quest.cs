using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestItem : Item {

    #region Constructors
    public QuestItem(string name, int id, int output, string desc, int value, int stack, int amount)
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
