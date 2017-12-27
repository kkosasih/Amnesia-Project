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

    // Move an object to a position over a set time
    public static IEnumerator LerpMovement (GameObject obj, Vector3 newPos, float time)
    {
        Vector3 oldPos = obj.transform.position;
        for (float timePassed = 0; timePassed < time; timePassed += Time.deltaTime)
        {
            obj.transform.position = Vector3.Lerp(oldPos, newPos, timePassed / time);
            yield return new WaitForEndOfFrame();
        }
        obj.transform.position = newPos;
    }

    // Play an animation completely within a time frame for an int
    public static IEnumerator PlayInTime(Animator anim, string name, int firstValue, int secondValue, float time)
    {
        anim.SetInteger(name, firstValue);
        anim.speed = 1.0f / time;
        yield return new WaitForSeconds(time);
        anim.SetInteger(name, secondValue);
    }
}
