using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropScript : Character
{
    public int SlimeAmount;
    public TileType priorType;
    public PlayerCharacter player;
    //public ItemDatabase database;
    public GameObject bag;
    public GameObject ui;
    public PickupInventory bagItems;
    public bool empty = false;

    // Use this for initialization
    protected override void Start()
    {
        ui = GameObject.Find("PickupInventory");
        bag = this.gameObject;
        bagItems = GetComponent<PickupInventory>();
        Destroy(healthSlider);
        bagItems.AddItemByID(2);
        GameController.map.tiles[currentTile].GetComponent<Tile>().type = TileType.Pickup;
    }

    protected override void Update()
    {
        base.Update();
        if (bagItems.emptyAfterTransfer)
        {
            Destroy(gameObject);
        }
    }
    /*
    public void AddItem(string name, int total)
    {
        for (int i = 0; i < 99; i++)
        {
            if (bagitems[i].itemName == name)
            {
                for (int j = 0; j < 25; j++)//Remember to increase depending on size of database
                {
                    if (name == ItemDatabase.items[j].itemName)
                    {
                        while (total > 0)
                        {
                            if (ItemDatabase.items[j].itemStack < bagitems[i].itemStackAmount)
                            {
                                bagitems[i].itemStackAmount += 1;
                                total -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < 99; i++)
        {
            if (bagitems[i].itemName == null)
            {
                for (int j = 0; j < 25; j++)// Remember to increase depending on size of database
                {
                    if (name == ItemDatabase.items[j].itemName)
                    {
                        bagitems[i] = ItemDatabase.items[j];
                    }

                    while (total > 0)
                    {
                        if (ItemDatabase.items[j].itemStack < bagitems[i].itemStackAmount)
                        {
                            bagitems[i].itemStackAmount += 1;
                            total -= 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    // Remove an item from the bag
    public void RemoveItem (int index)
    {
        bagitems.RemoveAt(index);
    }
    */
    // Get the horizontal distance from player; negative is player to right, positive is player to left
    private int HoriDistance()
    {
        return this.currentTile % GameController.map.width - player.currentTile % GameController.map.width;
    }

    // Get the vertical distance from player; negative is player below, positive is player above
    private int VertDistance()
    {
        return this.currentTile / GameController.map.width - player.currentTile / GameController.map.width;
    }
}