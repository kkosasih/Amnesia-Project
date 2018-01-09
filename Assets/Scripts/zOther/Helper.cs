using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper {

    // Returns a new list filled with an amount of items
    public static List<T> FillList<T>(int size, T item)
    {
        List<T> result = new List<T>();
        for (int i = 0; i < size; ++i)
        {
            result.Add(item);
        }
        return result;
    }

    // Makes the gameObject and its children visible or invisible
    public static void ChangeAllVisibility(GameObject obj, bool visible)
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
    public static IEnumerator LerpMovement(GameObject obj, Vector3 newPos, float time)
    {
        Vector3 oldPos = obj.transform.position;
        for (float timePassed = 0; timePassed < time; timePassed += Time.deltaTime)
        {
            obj.transform.position = Vector3.Lerp(oldPos, newPos, timePassed / time);
            yield return new WaitForEndOfFrame();
        }
        obj.transform.position = newPos;
    }

    // Play an animation completely within a time frame for an int vale
    public static IEnumerator PlayInTime(Animator anim, string name, int firstValue, int secondValue, float time)
    {
        anim.SetInteger(name, firstValue);
        anim.speed = 1.0f / time;
        yield return new WaitForSeconds(time);
        anim.SetInteger(name, secondValue);
    }

    // Play an animation completely within a time frame for a float vale
    public static IEnumerator PlayInTime(Animator anim, string name, float firstValue, float secondValue, float time)
    {
        anim.SetFloat(name, firstValue);
        anim.speed = 1.0f / time;
        yield return new WaitForSeconds(time);
        anim.SetFloat(name, secondValue);
    }

    // Play an animation completely within a time frame for a bool vale
    public static IEnumerator PlayInTime(Animator anim, string name, bool firstValue, bool secondValue, float time)
    {
        anim.SetBool(name, firstValue);
        anim.speed = 1.0f / time;
        yield return new WaitForSeconds(time);
        anim.SetBool(name, secondValue);
    }

    // Change the color of an image over a given time
    public static IEnumerator ChangeColorInTime(Image i, Color newColor, float time)
    {
        Color oldColor = i.color;
        for (float timePassed = 0; timePassed < time; timePassed += Time.deltaTime)
        {
            i.color = Color.Lerp(oldColor, newColor, timePassed / time);
            yield return new WaitForEndOfFrame();
        }
        i.color = newColor;
    }

    // Change the color of a sprite over a given time
    public static IEnumerator ChangeColorInTime(SpriteRenderer i, Color newColor, float time)
    {
        Color oldColor = i.color;
        for (float timePassed = 0; timePassed < time; timePassed += Time.deltaTime)
        {
            i.color = Color.Lerp(oldColor, newColor, timePassed / time);
            yield return new WaitForEndOfFrame();
        }
        i.color = newColor;
    }

    // Get the offset anchorMin of a gameObject
    public static Vector2 FixedAnchorMin (Transform t)
    {
        RectTransform rt = t.GetComponent<RectTransform>();
        float xResult = 1 - rt.anchorMin.x;
        float yResult = 1 - rt.anchorMin.y;
        for (Transform nt = t.parent; nt != null && nt.GetComponent<RectTransform>() != null; nt = nt.parent)
        {
            rt = nt.GetComponent<RectTransform>();
            xResult *= 1 - rt.anchorMin.x;
            yResult *= 1 - rt.anchorMin.y;
        }
        return new Vector2(1 - xResult, 1 - yResult);
    }

    // Get the offset anchorMax of a gameObject
    public static Vector2 FixedAnchorMax (Transform t)
    {
        RectTransform rt = t.GetComponent<RectTransform>();
        float xResult = rt.anchorMax.x;
        float yResult = rt.anchorMax.y;
        for (Transform nt = t.parent; nt != null && nt.GetComponent<RectTransform>() != null; nt = nt.parent)
        {
            rt = nt.GetComponent<RectTransform>();
            xResult *= rt.anchorMax.x;
            yResult *= rt.anchorMax.y;
        }
        return new Vector2(xResult, yResult);
    }
}
