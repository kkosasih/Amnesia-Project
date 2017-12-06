using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInventory : MonoBehaviour {
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	//public GUISkin skin;
	public GameObject Itemdata;
	public GameObject screen;
	public GameObject inventorySlots;
	public GameObject slot;
	public Texture2D slot_texture;
	public Texture2D equip_slot;
	//private bool showinventory = true;
	//private bool showtooltip = false;
	private ItemDatabase database;
	private bool draw = false;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < 21; i++) //remeber to increase based on database
		{
			slots.Add(new Item());
		}


		database = Itemdata.GetComponent<ItemDatabase>();
		//Starting Items
		for (int i = 0; i < database.items.Count; i++) {
			inventory.Add (database.items [i]);
		}
		for (int i = 0; i < slots.Count; i++) {
			GameObject newSlot = (GameObject)Instantiate (slot);
			newSlot.transform.SetParent (inventorySlots.transform);
			if (i < inventory.Count) {
				newSlot.GetComponent <Texture2D> ().UpdateExternalTexture (inventory [i].itemIcon.GetNativeTexturePtr ());
			}
		}

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.I))
			draw = !draw;
		if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Escape))
			draw = false;
		screen.SetActive (draw);
		inventorySlots.SetActive (draw);
	}
	/*
	private void OnGUI()
	{
		int shiftRight = 400;
		int shiftDown = 50;

		if (draw){
			int x = 0;
			int y = 0;
			for (int i = 0; i < slots.Count; i++) {
				if (x == 3) {
					x = 0;
					y++;
				}
				x++;
				if (i < inventory.Count)
					GUI.Label (new Rect (25*x + shiftRight, 25*y + shiftDown, 50, 50), inventory[i].itemIcon);
				else
					GUI.Label (new Rect (25*x + shiftRight, 25*y + shiftDown, 50, 50), slot_texture);
			}
			GUI.Label (new Rect (340, 25, 75, 50), "Equipment");
			GUI.Label (new Rect (125 + shiftRight, 25, 75, 50), "Item Quicklist");
			for (int i = 0; i < 3; i++) {
				GUI.Label (new Rect (shiftRight - 50, 50 * i + shiftDown, 50, 50), equip_slot);
				GUI.Label (new Rect (shiftRight + 125, 50 * i + shiftDown, 50, 50), equip_slot);
			}
		}

	}*/
	
}
