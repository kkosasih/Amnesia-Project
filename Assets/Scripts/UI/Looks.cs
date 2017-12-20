using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Looks
{
    public string itemName;
    public Texture2D looks;

    public Looks(string name)
    {
        itemName = name;
        looks = Resources.Load<Texture2D>("CharacterParts/" + name);
    }
}
