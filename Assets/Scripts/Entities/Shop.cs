using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    public int numOfItems;
    public List<Item> items;
    //private ItemDatabase database;

	// Use this for initialization
	void Start ()
    {
        items = new List<Item>();
        for (int i = 0; i < numOfItems; ++i)
        {
            items.Add(ItemDatabase.items[Random.Range(0, ItemDatabase.items.Count)]);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
