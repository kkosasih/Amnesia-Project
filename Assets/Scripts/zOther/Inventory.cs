using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int size;
    public List<Item> inventory = new List<Item>();
    public List<GameObject> slots;
    //public GUISkin skin;
    public GameObject Itemdata;
    //private bool showinventory = true;
    //private bool showtooltip = false;
    private ItemDatabase database;
    //private int slotsx = 7, slotsy = 4;
    //private string tooltip;

    //NOTE Most Commented out stuff are UI based

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < size; i++) //remember to increase based on database
        {
            inventory.Add(new Item());
            slots.Add(MakePanel(i));
        }
        database = Itemdata.GetComponent<ItemDatabase>();
        //Starting Items
        inventory[0] = database.items[0];
        //inventory[1] = database.items[1];
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            showinventory = !showinventory;
        }*/
    }

    /*void OnGUI()
    {
        tooltip = "";
        GUI.skin = skin;
        if(showinventory)
        {
            DrawInventory();
        }
        if(showtooltip)
        {
            GUI.Label(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 175, 100), tooltip, skin.GetStyle("labels"));
        }
    }*/

    public void AddItem(string name)
    {
        for (int i = 0; i < 17; i++)// Remeber to increase based on inventory size (NOTE_TO_SELF make a variable later) ******** -W
        {
            if (inventory[i].itemName == null)
            {
                for (int j = 0; j < 25; j++)// Remember to increase depending on size of database
                {
                    if (name == database.items[j].itemName)
                    {
                        inventory[i] = database.items[j];
                        break;
                    }
                    else //Debug else statement
                    {
                        Debug.Log("The Id is not in the database");
                        break;
                    }
                }
            }
            else
            {
                //Possible text saying no inventory space
                break;
            }
        }
    }

    // Add items by ID
    public void AddItemByID (int id)
    {
        for (int i = 0; i < 17; ++i)
        {
            if (inventory[i].itemName == null)
            {
                if (database.items.Count > id)
                {
                    inventory[i] = database.items[id];
                }
                else
                {
                    Debug.Log("The Id is not in the database");
                }
                return;
            }
        }
    }

    // Clears items in the inventory
    public void Clear ()
    {
        for (int i = 0; i < 17; ++i)
        {
            inventory[i] = new Item();
        }
    }

    // Make and return an inventory slot
    private GameObject MakeSlot (int index)
    {
        GameObject result = Instantiate(Resources.Load<GameObject>("GUI/ItemPanel"), transform);

        return result;
    }

    /*void DrawInventory()
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
                    if(slotRect.Contains(Event.current.mousePosition))
                    {
                        tooltip = "    " + slots[i].itemName + " (" + slots[i].itemtype + ")" + "\n\n    " + slots[i].itemDesc;
                        showtooltip = true;
                    }
                }
                if (tooltip == "")
                {
                    showtooltip = false;
                }
                i++;
            }
        }

        i = 17;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 1; x++)
            {
                Rect slotRect1 = new Rect((x * 60) + 115, (y * 60) + 100, 50, 50);
                GUI.Box(slotRect1, "", skin.GetStyle("slot"));
                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect1, slots[i].itemIcon);
                    if (slotRect1.Contains(Event.current.mousePosition))
                    {
                        tooltip = "    " + slots[i].itemName + " (" + slots[i].itemtype + ")" + "\n\n    " + slots[i].itemDesc;
                        showtooltip = true;
                    }
                }
                if (tooltip == "")
                {
                    showtooltip = false;
                }
                i++;
            }
        }

        i = 21;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 1; x++)
            {
                Rect slotRect2 = new Rect((x * 60) + 550, (y * 60) + 100, 50, 50);
                GUI.Box(slotRect2, "", skin.GetStyle("slot"));
                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect2, slots[i].itemIcon);
                    if (slotRect2.Contains(Event.current.mousePosition))
                    {
                        tooltip = "    " + slots[i].itemName + " (" + slots[i].itemtype + ")" + "\n\n    " + slots[i].itemDesc;
                        showtooltip = true;
                    }
                }
                if(tooltip == "")
                {
                    showtooltip = false;
                }
                i++;
            }
        }
    }*/
}
