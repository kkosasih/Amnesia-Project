using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemScreen : MonoBehaviour {
    private PickupInventory inventory;

    // Change the inventory that is shown
    public void ChangeInventory(PickupInventory newInv)
    {
        inventory = newInv;
    }

    // Take the item in the given slot and close if empty
    public void TakeItem (int index)
    {
        inventory.TransferItem(GameObject.Find("Inventory").GetComponent<Inventory>(), index);
        if (inventory.IsEmpty())
        {
            Close();
        }
    }

    // Take all items
    public void TakeAllItems ()
    {
        inventory.TransferAllItems(GameObject.Find("Inventory").GetComponent<Inventory>());
        Close();
    }

    // Open the inventory and stop player movement
    public void Open ()
    {
        ++GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().movementPreventions;
        GetComponent<UIPanel>().isOpen = true;
        inventory.UpdateImages();
    }

    // Close the inventory and continue player movement
    public void Close ()
    {
        --GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().movementPreventions;
        GetComponent<UIPanel>().isOpen = false;
    }
}
