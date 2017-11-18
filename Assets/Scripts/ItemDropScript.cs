using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropScript : MonoBehaviour {
    public int SlimeAmount;
    public ItemDatabase database;
    public List<Item> inventory;

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        for (int j = 0; j < 25; j++)// Remember to increase depending on size of database
        {
            if (name == database.items[j].itemName)
            {
                inventory[j] = database.items[j];
                break;
            }
        }
    }
}
