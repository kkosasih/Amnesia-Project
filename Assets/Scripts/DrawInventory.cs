using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInventory : MonoBehaviour {
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	//public GUISkin skin;
	public GameObject Itemdata;
	public Texture2D slot_texture;
	//private bool showinventory = true;
	//private bool showtooltip = false;
	private ItemDatabase database;
	private bool draw = false;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 25; i++) //remeber to increase based on database
		{
			slots.Add(new Item());
		}

		database = Itemdata.GetComponent<ItemDatabase>();
		//Starting Items
		for (int i = 0; i < database.items.Count; i++) {
			inventory.Add (database.items [i]);
		}

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.I))
			draw = true;
		if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Escape))
			draw = false;
	}
	private void OnGUI()
	{
		int shiftRight = 400;
		if (draw){
			int x = 0;
			int y = 0;
			for (int i = 0; i < slots.Count; i++) {
				if (x == 5) {
					x = 0;
					y++;
				}
				x++;
				if (i < inventory.Count)
					GUI.Label (new Rect (25*x + shiftRight, 25*y, 50, 50), inventory[i].itemIcon);
				else
					GUI.Label (new Rect (25*x + shiftRight, 25*y, 50, 50), slot_texture);
			
			}
		}

	}
	
}
