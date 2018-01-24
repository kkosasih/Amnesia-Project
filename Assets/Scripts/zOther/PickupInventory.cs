using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInventory : Inventory {
    public bool emptyAfterTransfer = false; // True only if the inventory is empty

    // Use this for initialization
    protected override void Start ()
    {
        //Starting Items

        //inventory[1] = database.items[1];
    }

    // Set up the number of items
    public void SetSize (int newSize)
    {
        size = newSize;
        for (int i = 0; i < newSize; i++) //remember to increase based on database
        {
            inventory.Add(new Item());
            slots.Add(GameObject.Find("PickupInventory").transform.GetChild(i).gameObject);
        }
    }

    // Transfer an item to another inventory
    public override void TransferItem (Inventory other, int toTransfer)
    {
        base.TransferItem(other, toTransfer);
        DeleteIfEmpty();
    }

    // Transfer all items to another inventory
    public override void TransferAllItems(Inventory other)
    {
        base.TransferAllItems(other);
        DeleteIfEmpty();
    }

    // Deletes self if the inventory is empty
    private void DeleteIfEmpty ()
    {
        foreach (Item i in inventory)
        {
            if (i.itemId != -1)
            {
                return;
            }
        }
        Destroy(GetComponent<SpriteRenderer>());
        GetComponent<Tile>().type = GetComponent<Tile>().startType;
        Destroy(this);
    }
}
