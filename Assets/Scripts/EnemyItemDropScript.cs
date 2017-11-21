using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDropScript : MonoBehaviour
{
    public GameObject Item;         //Temporary name of the instatiated GameObject
    public GameObject ItDatabase;   //Gameobject refering to the ItemDatabase
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
    }

    public void EnemyDied()
    {
        GameObject Item = (GameObject)Instantiate(Resources.Load("Prefab"), transform.position, transform.rotation); //Creates the Object
        Item.name = "Lootbag";
        Item.GetComponent<ItemDropScript>().SlimeAmount = Random.Range(lowmoney, highmoney); //Where it changes how much currency was dropped
        //Place Garanteed Drops here same as DroppingItems("*Put Item Here*");
        for (int i = 0; i < dropamount; i++)
        {
            tempname = ItDatabase.GetComponent<ItemDatabase>().randomdrop(this.name);
            for (int j = 0; j < 25; j++)//Remember to increase depending on size of database
            {
                if (tempname == ItDatabase.GetComponent<ItemDatabase>().items[j].itemName)
                {
                    randomamount = Random.Range(1, ItDatabase.GetComponent<ItemDatabase>().items[j].itemStack);
                }
            }
            Item.GetComponent<ItemDropScript>().AddItem(tempname, randomamount);
        }
        //Insert code for getting rid of the enemy GameObject and etc. of when he dies
    }
}