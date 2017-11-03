using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
