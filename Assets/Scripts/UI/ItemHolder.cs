using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour {
    public static int invIndex;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(UpdateItem(-1));
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<RectTransform>().position = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(UpdateItem(-1));
        }
    }

    // Update the item being held, nullify after a frame
    public IEnumerator UpdateItem (int index)
    {
        yield return new WaitForEndOfFrame();
        invIndex = index;
        Image i = GetComponent<Image>();
        if (index >= 0)
        {
            i.sprite = GameObject.Find("Inventory").GetComponent<Inventory>().inventory[index].itemIcon;
            i.color = Color.white;
        }
        else
        {
            i.sprite = null;
            i.color = Color.clear;
        }
    }
}
