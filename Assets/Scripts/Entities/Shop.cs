using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    #region Attributes
    public int numOfItems;      // Number of items the shop has
    public List<Item> items;    // The items that the shop has
                                //private ItemDatabase database;
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {
        items = new List<Item>();
        for (int i = 0; i < numOfItems; ++i)
        {
            items.Add(GameController.instance.Database.items[Random.Range(0, GameController.instance.Database.items.Count)]);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    #endregion
}
