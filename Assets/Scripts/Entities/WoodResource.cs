using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : Resource {

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < 10; ++i)
        {
            inventory.inventory[i] = ItemDatabase.items[8];
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
