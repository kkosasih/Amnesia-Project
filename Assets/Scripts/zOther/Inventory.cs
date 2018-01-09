using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            slots.Add(MakeSlot(i));
        }
        database = Itemdata.GetComponent<ItemDatabase>();
        //Starting Items
        AddItemByID(0);
        for (int i = 0; i < 5; ++i)
        {
            AddItemByID(2);
        }
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
        if (id >= database.items.Count)
        {
            Debug.Log("The Id is not in the database");
            return;
        }
        for (int i = 0; i < size; ++i)
        {
            if (inventory[i].itemId == id && inventory[i].itemStackAmount < inventory[i].itemStack)
            {
                ++inventory[i].itemStackAmount;
                UpdateImages();
                break;
            }
            else if (inventory[i].itemId == -1)
            {
                inventory[i] = database.items[id];
                UpdateImages();
                break;
            }
        }
    }

    // Switch two items in the inventory
    public void SwitchItems (int index1, int index2)
    {
        Item tempItem = new Item(inventory[index1]);
        inventory[index1] = inventory[index2];
        inventory[index2] = tempItem;
        UpdateImages();
    }

    // Clears items in the inventory
    public void Clear ()
    {
        for (int i = 0; i < size; ++i)
        {
            inventory[i] = new Item();
        }
    }

    // Make and return an inventory slot
    private GameObject MakeSlot (int index)
    {
        GameObject result = Instantiate(Resources.Load<GameObject>("GUI/ItemPanel"), transform);
        result.GetComponent<ItemPanel>().invIndex = index;
        RectTransform r = result.GetComponent<RectTransform>();
        r.anchorMin = new Vector2(0.08f * (index % 10) + 0.1f, 1 - (0.267f * (index / 10 + 1) + 0.1f));
        r.anchorMax = new Vector2(0.08f * (index % 10 + 1) + 0.1f, 1 - (0.267f * (index / 10) + 0.1f));
        return result;
    }

    // Update the visual slots
    private void UpdateImages ()
    {
        for (int i = 0; i < size; ++i)
        {
            transform.GetChild(i).Find("ItemImage").GetComponent<Image>().sprite = inventory[i].itemIcon;
            if (inventory[i].itemStack > 1)
            {
                transform.GetChild(i).Find("ItemAmount").GetComponent<Text>().text = inventory[i].itemStackAmount.ToString();
            }
            else
            {
                transform.GetChild(i).Find("ItemAmount").GetComponent<Text>().text = "";
            }
        }
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
