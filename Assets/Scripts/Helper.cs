using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper {

    // Returns a new list filled with an amount of items
    public static List<T> FillList<T> (int size, T item)
    {
        List<T> result = new List<T>();
        for (int i = 0; i < size; ++i)
        {
            result.Add(item);
        }
        return result;
    }

    // Makes the gameObject and its children visible or invisible
    public static void ChangeAllVisibility (GameObject obj, bool visible)
    {
        if (obj.GetComponent<Image>() != null)
        {
            obj.GetComponent<Image>().enabled = visible;
        }
        else if (obj.GetComponent<SpriteRenderer>() != null)
        {
            obj.GetComponent<SpriteRenderer>().enabled = visible;
        }
        else if (obj.GetComponent<Text>() != null)
        {
            obj.GetComponent<Text>().enabled = visible;
        }
        for (int i = 0; i < obj.transform.childCount; ++i)
        {
            ChangeAllVisibility(obj.transform.GetChild(i).gameObject, visible);
        }
    }
}
