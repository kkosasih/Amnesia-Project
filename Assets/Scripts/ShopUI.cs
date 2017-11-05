using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
    public Shop currentShop = null;
    public bool isOpen = false;
    private GameObject window;

	// Use this for initialization
	void Start ()
    {
        window = GameObject.FindWithTag("ShopWindow");
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (isOpen && !window.activeSelf)
        {
            window.SetActive(true);
        }
        else if (!isOpen && window.activeSelf)
        {
            window.SetActive(false);
        }
	}

    // Set the items in the store
    public void SetShop (Shop shop)
    {
        for (int itemsToKeep = 0; itemsToKeep < window.transform.childCount;)
        {
            if (window.transform.GetChild(itemsToKeep).GetComponent<Button>() != null)
            {
                Destroy(window.transform.GetChild(itemsToKeep));
            }
            else
            {
                ++itemsToKeep;
            }
        }
        currentShop = shop;
        for (int i = 0; i < currentShop.items.Count; ++i)
        {
            GameObject g = (GameObject)Instantiate(Resources.Load("ShopEntry"), window.transform);
            RectTransform rt = g.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.1f, i * 0.9f / currentShop.items.Count);
            rt.anchorMax = new Vector2(0.9f, (i + 1) * 0.9f / currentShop.items.Count);
            g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/" + currentShop.items[i].itemName);
            g.transform.GetChild(1).GetComponent<Text>().text = currentShop.items[i].itemName;
            g.transform.GetChild(2).GetComponent<Text>().text = currentShop.items[i].itemValue.ToString();
            g.transform.GetChild(3).GetComponent<Text>().text = currentShop.items[i].itemDesc;
        }
    }

    // Exit the shop
    public void ExitShop ()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().movementEnabled = true;
        isOpen = false;
    }
}
