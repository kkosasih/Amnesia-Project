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

    // Get the offset anchorMin and anchorMax of an object
    public static List<Vector2> FixedAnchorMinMax (Transform t)
    {
        List<Transform> tree = GetAncestors(t);
        Vector2 tempMin = new Vector2(0, 0);
        Vector2 tempMax = new Vector2(1, 1);
        foreach (Transform ti in tree)
        {
            RectTransform rt = ti.GetComponent<RectTransform>();
            if (rt != null && !ti.CompareTag("Canvas"))
            {
                Vector2 tempMin2 = new Vector2((tempMax.x - tempMin.x) * rt.anchorMin.x + tempMin.x, (tempMax.y - tempMin.y) * rt.anchorMin.y + tempMin.y);
                Vector2 tempMax2 = new Vector2((tempMax.x - tempMin.x) * rt.anchorMax.x + tempMin.x, (tempMax.y - tempMin.y) * rt.anchorMax.y + tempMin.y);
                tempMin = tempMin2;
                tempMax = tempMax2;
            }
        }
        return new List<Vector2> { tempMin, tempMax };
    }

    // Return the ancestors of the object's transform in chronological order
    public static List<Transform> GetAncestors (Transform t)
    {
        List<Transform> result = new List<Transform>();
        for (Transform ti = t; ti != null; ti = ti.parent)
        {
            result.Add(ti);
        }
        result.Reverse();
        return result;
    }

    // Reverses a list in chunks
    public static List<T> ReverseInChunks<T> (List<T> list, int chunkSize)
    {
        List<T> result = new List<T>();
        List<List<T>> chunkedResult = new List<List<T>>();
        for (int i = 0; i < list.Count; i += chunkSize)
        {
            List<T> toAdd = new List<T>();
            for (int j = i; j < i + chunkSize && j < list.Count; ++j)
            {
                toAdd.Add(list[j]);
            }
            chunkedResult.Add(toAdd);
        }
        chunkedResult.Reverse();
        foreach (List<T> l in chunkedResult)
        {
            result.AddRange(l);
        }
        return result;
    }

    // Gives a random color
    public static Color RandomColor (bool useAlpha)
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), useAlpha ? Random.Range(0f, 1f) : 1);
    }
}
