using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropScript : Character
{
    public int SlimeAmount;
    public PlayerCharacter player;
    public ItemDatabase database;
    public GameObject bag;
    public GameObject ui;
    public List<Item> bagitems = new List<Item>();

    // Use this for initialization
    protected override void Start()
    {
        bag = this.gameObject;
        //Destroy(healthSlider.gameObject);
    }

    protected override void Update()
    {
        base.Update();
        GameController.map.tiles[currentTile].GetComponent<Tile>().type = TileType.Pickup;
        if (HoriDistance() + VertDistance() <= 1)
        {
            player.item = bag;
        }
    }

    public void AddItem(string name, int total)
    {
        for (int i = 0; i < 99; i++)
        {
            if (bagitems[i].itemName == name)
            {
                for (int j = 0; j < 25; j++)//Remember to increase depending on size of database
                {
                    if (name == database.items[j].itemName)
                    {
                        while (total > 0)
                        {
                            if (database.items[j].itemStack < bagitems[i].itemStackAmount)
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
                    if (name == database.items[j].itemName)
                    {
                        bagitems[i] = database.items[j];
                    }

                    while (total > 0)
                    {
                        if (database.items[j].itemStack < bagitems[i].itemStackAmount)
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