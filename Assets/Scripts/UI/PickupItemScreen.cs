using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemScreen : MonoBehaviour {
    public PickupInventory inventory;

    // Change the inventory that is shown
    public void ChangeInventory (PickupInventory newInv)
    {
        inventory = newInv;
        inventory.UpdateImages();
    }

    // Take the item in the given slot and close if empty
    public void TakeItem (int index)
    {
        inventory.TransferItem(GameObject.Find("Inventory").GetComponent<Inventory>(), index);
        if (inventory == null)
        {
            GetComponent<UIPanel>().isOpen = false;
        }
    }
}
