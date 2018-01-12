using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDropScript : MonoBehaviour
{
    public GameObject Item;         //Temporary name of the instatiated GameObject
    //public GameObject ItDatabase;   //Gameobject refering to the ItemDatabase
    public GameObject monster;
    public GameObject inventory;
    public GameController controller;
    public PlayerCharacter player;
    public string tempname;         //Temporary name of object being dropped
    public int dropamount;
    public int lowitems;
    public int highitems;
    public int lowmoney;
    public int highmoney;
    private int randomamount;

    void Start()
    {
        controller = GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
        inventory = GameObject.Find("Canvas");
        monster = this.gameObject;
    }

    public void EnemyDied(bool pickuptile, string name)
    {
        inventory.GetComponent<QuestTracking>().questobj(name);
        if (GameController.map.tiles[GetComponent<Monster>().currentTile].GetComponent<Tile>().type != TileType.Pickup)
        {
            GameObject Item = Instantiate(Resources.Load<GameObject>("Prefab")); //Creates the Object
            Item.name = "Lootbag";
            Item.GetComponent<Transform>().position = this.GetComponent<Monster>().Mlocation();
            Item.GetComponent<ItemDropScript>().currentTile = this.GetComponent<Monster>().currentTile;
            Item.GetComponent<ItemDropScript>().SlimeAmount = Random.Range(lowmoney, highmoney); //Where it changes how much currency was dropped
            for (int i = 0; i < dropamount; i++)
            {
                tempname = ItemDatabase.randomdrop(this.name);
                for (int j = 0; j < ItemDatabase.items.Count; j++)//Remember to increase depending on size of database
                {
                    if (tempname == ItemDatabase.items[j].itemName)
                    {
                        randomamount = Random.Range(1, ItemDatabase.items[j].itemStack);
                    }
                }
                for (int j = 0; j < randomamount; ++i)
                {
                    Item.GetComponent<ItemDropScript>().bagItems.AddItem(tempname);
                }
            }
        }
        else
        {
            //Something that gets the a reference to a the lootbag;
            Item.GetComponent<ItemDropScript>().SlimeAmount += Random.Range(lowmoney, highmoney); //Where it changes how much currency was dropped
            for (int i = 0; i < dropamount; i++)
            {
                tempname = ItemDatabase.randomdrop(this.name);
                for (int j = 0; j < ItemDatabase.items.Count; j++)//Remember to increase depending on size of database
                {
                    if (tempname == ItemDatabase.items[j].itemName)
                    {
                        randomamount = Random.Range(1, ItemDatabase.items[j].itemStack);
                    }
                }
                for (int j = 0; j < randomamount; ++i)
                {
                    Item.GetComponent<ItemDropScript>().bagItems.AddItem(tempname);
                }
            }
        }
    }
}