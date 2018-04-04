using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public int size;                                            // The number of items in the inventory
    public int columns;                                         // The number of columns to use in the UI
    public float margin;                                        // The margin for anchors to use in the UI
    public List<Item> inventory = new List<Item>();             // The actual items
    public List<GameObject> slots = new List<GameObject>();     // The UI items to manipulate
    //public GUISkin skin;
    //public GameObject Itemdata;
    //private bool showinventory = true;
    //private bool showtooltip = false;
    //protected static ItemDatabase database;
    //private int slotsx = 7, slotsy = 4;
    //private string tooltip;

    //NOTE Most Commented out stuff are UI based

    // Use this for initialization
    protected virtual void Start()
    {
        for (int i = 0; i < size; i++) //remember to increase based on database
        {
            inventory.Add(new Item());
            slots.Add(MakeSlot(i));
        }
        //Starting Items
        
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

    // Adds item from the database by name
    public void AddItem(string name)
    {
        for (int i = 0; i < 17; i++)// Remeber to increase based on inventory size (NOTE_TO_SELF make a variable later) ******** -W
        {
            if (inventory[i].itemName == null)
            {
                // Search database by name
                for (int j = 0; j < ItemDatabase.items.Count; j++)// Remember to increase depending on size of database
                {
                    if (name == ItemDatabase.items[j].itemName)
                    {
                        inventory[i] = ItemDatabase.items[j];
                        return;
                    }
                    /*else //Debug else statement
                    {

                    }*/
                }
                Debug.Log("The Id is not in the database");
                return;
            }
            /*else
            {
                //Possible text saying no inventory space
                break;
            }*/
        }
    }

    // Add items from database by ID
    public void AddItemByID (int id)
    {
        if (id >= ItemDatabase.items.Count)
        {
            Debug.Log("The Id is not in the database");
            return;
        }
        // Add a new item (so the database doesn't change)
        AddItemByItem(new Item(ItemDatabase.items[id]));
    }

    // Add items by raw items
    public void AddItemByItem (Item toAdd)
    {
        for (int i = 0; i < size; ++i)
        {
            // Item stacks
            if (inventory[i].itemId == toAdd.itemId)
            {
                if (inventory[i].itemStack - inventory[i].itemStackAmount >= toAdd.itemStackAmount)
                {
                    inventory[i].itemStackAmount += toAdd.itemStackAmount;
                    break;
                }
                else
                {
                    toAdd.itemStackAmount -= inventory[i].itemStackAmount - inventory[i].itemStack;
                    inventory[i].itemStackAmount = inventory[i].itemStack;
                }
            }
            else if (inventory[i].itemId == -1)
            {
                inventory[i] = toAdd;
                break;
            }
        }
        UpdateImages();
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
    protected virtual GameObject MakeSlot (int index)
    {
        GameObject result = Instantiate(Resources.Load<GameObject>("GUI/ItemPanel"), transform);
        result.GetComponent<ItemPanel>().invIndex = index;
        RectTransform r = result.GetComponent<RectTransform>();
        r.anchorMin = new Vector2((1 - 2 * margin) / columns * (index % columns) + margin, 1 - ((1 - 2 * margin) / (size / columns) * (index / columns + 1) + margin));
        r.anchorMax = new Vector2((1 - 2 * margin) / columns * (index % columns + 1) + margin, 1 - ((1 - 2 * margin) / (size / columns) * (index / columns) + margin));
        return result;
    }

    // Update the visual slots to the images of the items
    public void UpdateImages ()
    {
        for (int i = 0; i < size; ++i)
        {
            slots[i].transform.Find("ItemImage").GetComponent<Image>().sprite = inventory[i].itemIcon;
            if (inventory[i].itemStack > 1)
            {
                slots[i].transform.Find("ItemAmount").GetComponent<Text>().text = inventory[i].itemStackAmount.ToString();
            }
            else
            {
                slots[i].transform.Find("ItemAmount").GetComponent<Text>().text = "";
            }
        }
    }

    // Transfer an item to another inventory
    public virtual void TransferItem (Inventory other, int toTransfer)
    {
        other.AddItemByItem(inventory[toTransfer]);
        inventory[toTransfer] = new Item();
        UpdateImages();
    }

    // Transfer all items to another inventory
    public virtual void TransferAllItems (Inventory other)
    {
        foreach (Item i in inventory)
        {
            if (i.itemId >= 0)
            {
                other.AddItemByItem(i);
            }
        }
        Clear();
        UpdateImages();
    }

    // Return the amount of slots not empty
    public int SlotCount ()
    {
        int result = 0;
        for (int i = 0; i < inventory.Count; ++i)
        {
            if (inventory[i] != new Item())
            {
                ++result;
            }
        }
        return result;
    }

    // Move any items to fill in gaps
    public void MoveSlots ()
    {
        for (int i = 0; i < inventory.Count; ++i)
        {
            if (inventory[i] == new Item())
            {
                for (int j = i + 1; j < inventory.Count; ++j)
                {
                    if (inventory[j] != new Item())
                    {
                        SwitchItems(i, j);
                        break;
                    }
                    if (j == inventory.Count - 1)
                    {
                        return;
                    }
                }
            }
        }
    }

    // Checks if the inventory is empty
    public bool IsEmpty ()
    {
        foreach (Item i in inventory)
        {
            if (i.itemId >= 0)
            {
                return false;
            }
        }
        return true;
    }

    // Deletes self if the inventory is empty
    public void DeleteIfEmpty ()
    {
        foreach (Item i in inventory)
        {
            if (i.itemId != -1)
            {
                return;
            }
        }
        GetComponent<StaticObject>().Die();
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
