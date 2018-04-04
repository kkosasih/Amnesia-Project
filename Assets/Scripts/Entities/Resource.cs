using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : StaticObject {
    public Inventory inventory; // The inventory of the object

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

    // Get a random object from the inventory
    protected void GetRandomObject ()
    {
        int index = Random.Range(0, inventory.SlotCount());
        GameObject.Find("Canvas").GetComponent<QuestTracking>().questobj(inventory.inventory[index].itemName);
        inventory.TransferItem(PlayerCharacter.instance.inventory, index);
        PlayerCharacter.instance.interaction = GameController.map.FindInteractible();
        inventory.DeleteIfEmpty();
        inventory.MoveSlots();
    }
}
