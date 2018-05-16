using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectChild : StaticObject {
    #region Attributes

    #endregion
    private Inventory inventory;    // The inventory of the parent to get resources from
	#region Properties
	
	#endregion

	#region Event Functions
	// Awake is called before Start
	private void Awake ()
	{
        inventory = GetComponentInParent<Inventory>();
        Interactible i = GetComponent<Interactible>();
        if (i != null)
        {
            GetComponent<Interactible>().interact = GetRandomObject;
        }
	}

	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
	}
    #endregion

    #region Methods
    // Get a random object from the inventory
    protected void GetRandomObject ()
    {
        int slotCount = inventory.SlotCount();
        int index = Random.Range(0, slotCount);
        GameObject.Find("Canvas").GetComponent<QuestTracking>().questobj(inventory.Items[index].itemName);
        inventory.TransferItem(PlayerCharacter.instance.Inven, index);
        PlayerCharacter.instance.UpdateInteraction();
        inventory.DeleteIfEmpty();
        inventory.MoveSlots();
    }
    #endregion

    #region Coroutines

    #endregion
}
