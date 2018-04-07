using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : Resource {

    #region Event Functions
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < 10; ++i)
        {
            inventory.Items[i] = new Item(ItemDatabase.items[8]);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    #endregion
}
