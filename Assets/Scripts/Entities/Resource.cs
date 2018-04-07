using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : StaticObject {
    #region Attributes
    protected Inventory inventory;  // The inventory of the object
    #endregion

    #region Event Functions
    protected virtual void Awake ()
    {
        GetComponent<Interactible>().interact = GetRandomObject;
        inventory = GetComponent<Inventory>();
    }

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
    // Get a random object from the inventory
    protected void GetRandomObject ()
    {
        int index = Random.Range(0, inventory.SlotCount());
        GameObject.Find("Canvas").GetComponent<QuestTracking>().questobj(inventory.Items[index].itemName);
        inventory.TransferItem(PlayerCharacter.instance.Inven, index);
        PlayerCharacter.instance.UpdateInteraction();
        inventory.DeleteIfEmpty();
        inventory.MoveSlots();
    }
    #endregion
}
