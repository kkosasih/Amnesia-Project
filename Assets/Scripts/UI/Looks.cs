using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Looks
{
    public string itemName;
    public Sprite looks;

    public Looks(string name)
    {
        itemName = name;
        looks = Resources.Load<Sprite>("CharacterParts/" + name);
    }
}
