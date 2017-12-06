using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDropScript : MonoBehaviour
{
    public GameObject Item;         //Temporary name of the instatiated GameObject
    public GameObject ItDatabase;   //Gameobject refering to the ItemDatabase
    public GameObject monster;
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
        ItDatabase = GameObject.Find("Item Database");
        controller = GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
        monster = this.gameObject;
    }

    public void EnemyDied()
    {
        GameObject Item = (GameObject)Instantiate(Resources.Load("Prefab")); //Creates the Object
        Item.name = "Lootbag";
        Item.GetComponent<ItemDropScript>().SlimeAmount = Random.Range(lowmoney, highmoney); //Where it changes how much currency was dropped
        for (int i = 0; i < dropamount; i++)
        {
            tempname = ItDatabase.GetComponent<ItemDatabase>().randomdrop(this.name);
            for (int j = 0; j < ItDatabase.GetComponent<ItemDatabase>().items.Count; j++)//Remember to increase depending on size of database
            {
                if (tempname == ItDatabase.GetComponent<ItemDatabase>().items[j].itemName)
                {
                    randomamount = Random.Range(1, ItDatabase.GetComponent<ItemDatabase>().items[j].itemStack);
                }
            }
            Item.GetComponent<ItemDropScript>().AddItem(tempname, randomamount);
        }
        Item.GetComponent<Transform>().position = new Vector3(0, monster.GetComponent<Monster>().currentTile % controller.map.width, monster.GetComponent<Monster>().currentTile / controller.map.width);
    }
}