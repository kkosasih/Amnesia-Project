using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public GUISkin skin;
    public int[] Tools;
    public int[] Consumables;
    private bool showinventory = true;
    private ItemDatabase database;
    private int slotsx = 7, slotsy = 4;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        //Starting Items
        inventory[0] = database.items[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("i"))
        {

        }
        if (Input.GetButtonDown("e"))
        {

        }
    }

    void OnGUI()
    {
        GUI.skin = skin;
        if(showinventory)
        {
            DrawInventory();
        }
        for(int i = 0; i < inventory.Count; i++)
        {
            GUI.Label(new Rect(10, i * 20, 200, 50), inventory[i].itemName);
        }
    }

    void DrawInventory()
    {
        int i = 0;
        for (int y = 0; y < slotsy; y++)
        {
            for (int x = 0; x < 5; x++)    
            {
                Rect slotRect = new Rect((x * 60)+ 210,(y * 60)+100, 50, 50);
                GUI.Box(slotRect, "", skin.GetStyle("slot"));
                slots[i] = inventory[i];
                if(slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[i].itemIcon);
                }
                i++;
            }
        }

        i = 0;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 1; x++)
            {
                Rect slotRect1 = new Rect((x * 60) + 115, (y * 60) + 100, 50, 50);
                GUI.Box(slotRect1, "", skin.GetStyle("slot"));
                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect1, slots[i].itemIcon);
                }
                i++;
            }
        }

        i = 0;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 1; x++)
            {
                Rect slotRect2 = new Rect((x * 60) + 550, (y * 60) + 100, 50, 50);
                GUI.Box(slotRect2, "", skin.GetStyle("slot"));
                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect2, slots[i].itemIcon);
                }
                i++;
            }
        }
    }
}
