using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
    #region Attributes
    public Shop currentShop = null; // The shop attached to the ui
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

	}
    #endregion

    #region Methods
    // Set the items in the store
    public void SetShop (Shop shop)
    {
        // Remove past items
        for (int i = 0; i < transform.Find("Items").childCount; ++i)
        {
            Destroy(transform.Find("Items").GetChild(i).gameObject);
        }
        // Set the shop and ui depending on number of items
        currentShop = shop;
        for (int i = 0; i < currentShop.items.Count; ++i)
        {
            GameObject g = (GameObject)Instantiate(Resources.Load("ShopEntry"), transform.Find("Items").transform);
            RectTransform rt = g.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.1f, i * 0.9f / currentShop.items.Count + 0.1f);
            rt.anchorMax = new Vector2(0.9f, (i + 1) * 0.9f / currentShop.items.Count + 0.1f);
            g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/" + currentShop.items[i].itemName);
            g.transform.GetChild(1).GetComponent<Text>().text = currentShop.items[i].itemName;
            g.transform.GetChild(2).GetComponent<Text>().text = currentShop.items[i].itemValue.ToString();
            g.transform.GetChild(3).GetComponent<Text>().text = currentShop.items[i].itemDesc;
        }
    }
    
    // Enter the shop
    public void EnterShop ()
    {
        ++GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().movementPreventions;
        GetComponent<UIPanel>().isOpen = true;
    }

    // Exit the shop
    public void ExitShop ()
    {
        --GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().movementPreventions;
        GetComponent<UIPanel>().isOpen = false;
    }
    #endregion
}
