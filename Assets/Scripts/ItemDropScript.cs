using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropScript : MonoBehaviour
{
    public int SlimeAmount;
    public float xdis = 0;
    public float ydis = 0;
    public PlayerCharacter player;
    public ItemDatabase database;
    public GameController controller;
    public GameObject bag;
    public GameObject ui;
    public List<Item> bagitems = new List<Item>();

    // Use this for initialization
    void Start()
    {
        bag = this.gameObject;
        controller = GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
        ui = GameObject.Find("InteractionIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        xdis = HoriDistance();
        ydis = VertDistance();
        if(((xdis >= -100) && (xdis <= 100)) && ((ydis >= -100) && (ydis <= 100)))
        {
            ui.SetActive(true);
        }
        else
        {
            ui.SetActive(false);
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
    private float HoriDistance()
    {
        return this.GetComponent<Rigidbody2D>().transform.position.x - player.GetComponent<Rigidbody2D>().transform.position.x ;
    }

    // Get the vertical distance from player; negative is player below, positive is player above
    private float VertDistance()
    {
        return this.GetComponent<Rigidbody2D>().transform.position.y - player.GetComponent<Rigidbody2D>().transform.position.y;
    }
}