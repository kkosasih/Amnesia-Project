using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPanel : MonoBehaviour {
    #region Attributes
    public static ItemHolder holder;    // The holder assigned
    public int invIndex;                // The index assigned to the panel
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {
        holder = GameObject.Find("ItemHolder").GetComponent<ItemHolder>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (Input.GetMouseButtonDown(0) && MouseIsTouching())
        {
            StartCoroutine(holder.UpdateItem(invIndex));
        }
        else if (Input.GetMouseButtonUp(0) && ItemHolder.invIndex >= 0 && MouseIsTouching())
        {
            GameObject.Find("Inventory").GetComponent<Inventory>().SwitchItems(ItemHolder.invIndex, invIndex);
        }
        */
        // If clicked, update holder to this item
        if (Input.GetMouseButtonDown(0) && MouseIsTouching())
        {
            StartCoroutine(holder.UpdateItem(invIndex));
        }
        else if (Input.GetMouseButtonUp(0) && MouseIsTouching() && ItemHolder.invIndex >= 0)
        {
            GameObject.Find("Inventory").GetComponent<Inventory>().SwitchItems(ItemHolder.invIndex, invIndex);
        }
	}
    #endregion

    #region Methods
    // Returns true if the mouse is over the panel
    private bool MouseIsTouching ()
    {
        List<Vector2> bounds = Helper.FixedAnchorMinMax(transform);
        return GetComponent<Image>().enabled && Input.mousePosition.x >= Screen.width * bounds[0].x && Input.mousePosition.x < Screen.width * bounds[1].x
            && Input.mousePosition.y >= Screen.height * bounds[0].y && Input.mousePosition.y < Screen.height * bounds[1].y;
    }
    #endregion
}
