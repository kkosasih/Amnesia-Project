using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInventory : Inventory {
    #region Attributes

    #endregion
    
    #region Properties

    #endregion

    //NOTE Most Commented out stuff are UI based

    #region Event Functions
    protected override void Awake ()
    {
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
    
    #endregion
}
