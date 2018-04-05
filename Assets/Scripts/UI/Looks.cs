using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Looks
{
    #region Attributes
    public string itemName;
    public Sprite looks;
    #endregion

    #region Constructors
    public Looks(string name)
    {
        itemName = name;
        looks = Resources.Load<Sprite>("CharacterParts/" + name);
    }
    #endregion
}
