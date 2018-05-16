using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInventory : Inventory {
    #region Attributes
    private int emptySlotCount;
    #endregion
    
    #region Properties

    #endregion

    //NOTE Most Commented out stuff are UI based

    #region Event Functions
    protected override void Awake ()
    {
        for (int i = 0; i < size; i++) //remember to increase based on database
        {
            items.Add(new Item());
        }
        //Starting Items

        //inventory[1] = database.items[1];
    }

    // Use this for initialization
    protected override void Start ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            showinventory = !showinventory;
        }*/
    }
    #endregion

    #region Methods
    // Transfer an item to another inventory
    //public override void TransferItem (Inventory other, int toTransfer)
    //{
    //    other.AddItemByItem(items[toTransfer]);
    //    items[toTransfer] = new Item();
    //}

    public override void TransferItem (Inventory other, int toTransfer)
    {
        other.AddItemByItem(items[toTransfer]);
        emptySlotCount++;
    }

    public override void DeleteIfEmpty()
    {
        if (emptySlotCount == slotCount)
        {
            GetComponent<StaticObject>().Die();
        }
    }
    #endregion
}
