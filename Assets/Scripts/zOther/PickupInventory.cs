using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupInventory : Inventory {
    public bool emptyAfterTransfer = false;

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

    // Transfer an item to another inventory
    public override void TransferItem (Inventory other, int toTransfer)
    {
        other.AddItemByItem(inventory[toTransfer]);
        inventory[toTransfer] = new Item();
        UpdateImages();
        if (IsEmpty())
        {
            emptyAfterTransfer = true;
        }
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
        for (int i = 0; i < size; ++i)
        {
            slots[i].transform.Find("ItemImage").GetComponent<Image>().sprite = inventory[i].itemIcon;
            if (inventory[i].itemStack > 1)
            {
                slots[i].transform.GetChild(i).Find("ItemAmount").GetComponent<Text>().text = inventory[i].itemStackAmount.ToString();
            }
            else
            {
                slots[i].transform.GetChild(i).Find("ItemAmount").GetComponent<Text>().text = "";
            }
        }
    }
}
