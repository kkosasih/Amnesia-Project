using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
    public Shop currentShop = null;
    public bool isOpen = false;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        Image i = GetComponent<Image>();
		if (isOpen && !i.IsActive())
        {
            Helper.ChangeAllVisibility(gameObject, true);
        }
        else if (!isOpen && i.IsActive())
        {
            Helper.ChangeAllVisibility(gameObject, false);
        }
	}

    // Set the items in the store
    public void SetShop (Shop shop)
    {
        for (int i = 0; i < transform.Find("Items").childCount; ++i)
        {
            Destroy(transform.Find("Items").GetChild(i).gameObject);
        }
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

    // Exit the shop
    public void ExitShop ()
    {
        PlayerCharacter player = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        player.movementEnabled = true;
        player.Move(player.lastTile);
        isOpen = false;
    }
}
