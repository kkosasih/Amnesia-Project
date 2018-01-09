using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPanel : MonoBehaviour {
    public static ItemHolder holder;
    public int invIndex;

	// Use this for initialization
	void Start ()
    {
        holder = GameObject.Find("ItemHolder").GetComponent<ItemHolder>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && MouseIsTouching())
        {
            StartCoroutine(holder.UpdateItem(invIndex));
        }
        else if (Input.GetMouseButtonUp(0) && ItemHolder.invIndex >= 0 && MouseIsTouching())
        {
            GameObject.Find("Inventory").GetComponent<Inventory>().SwitchItems(ItemHolder.invIndex, invIndex);
        }
	}

    // Returns true if the mouse is over the panel
    private bool MouseIsTouching ()
    {
        Debug.Log("Tile " + invIndex.ToString() + ": " + Helper.FixedAnchorMin(transform).ToString() + ", " + Helper.FixedAnchorMax(transform).ToString());
        return Input.mousePosition.x >= Screen.width * Helper.FixedAnchorMin(transform).x && Input.mousePosition.x < Screen.width * Helper.FixedAnchorMax(transform).x
            && Input.mousePosition.y >= Screen.height * Helper.FixedAnchorMin(transform).y && Input.mousePosition.y < Screen.height * Helper.FixedAnchorMax(transform).y;
    }
    
    // 
}
