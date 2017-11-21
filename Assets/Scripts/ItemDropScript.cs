using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropScript : MonoBehaviour
{
    public int SlimeAmount;
    public ItemDatabase database;
    public List<Item> bagitems = new List<Item>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}