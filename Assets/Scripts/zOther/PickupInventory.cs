using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupInventory : Inventory {
    // Use this for initialization
    protected override void Start ()
    {
        for (int i = 0; i < size; i++) //remember to increase based on database
        {
            inventory.Add(new Item());
            slots.Add(GameObject.Find("PickupInventory").transform.GetChild(i).gameObject);
        }
        //Starting Items

        //inventory[1] = database.items[1];
    }

    // Checks if the inventory is empty
    public bool IsEmpty ()
    {
        foreach (Item i in inventory)
        {
            if (i.itemId != -1)
            {
                return false;
            }
        }
        return true;
    }

    // Update the pickup inventory
    public override void UpdateImages ()
    {
        Transform pickupInv = GameObject.Find("PickupInventory").transform;
        for (int i = 0; i < size; ++i)
        {
            pickupInv.GetChild(i).Find("ItemImage").GetComponent<Image>().sprite = inventory[i].itemIcon;
            if (inventory[i].itemStack > 1)
            {
                pickupInv.GetChild(i).Find("ItemAmount").GetComponent<Text>().text = inventory[i].itemStackAmount.ToString();
            }
            else
            {
                pickupInv.GetChild(i).Find("ItemAmount").GetComponent<Text>().text = "";
            }
        }
    }
}
