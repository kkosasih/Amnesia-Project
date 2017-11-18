using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDropScript : MonoBehaviour {
    public GameObject Item;         //Temporary name of the instatiated GameObject
    public GameObject ItDatabase;   //Gameobject refering to the ItemDatabase
    public string tempname;         //Temporary name of object being dropped
    public int dropamount;
    public int lowitems;
    public int highitems;
    public int lowmoney;
    public int highmoney;

    void Start()
    {
        ItDatabase = GameObject.Find("Item Database");
    }

    public void EnemyDied()
    {
        DroppingItems("slime"); //Slime or whatever the currency is this is where it's being dropped
        //Place Garanteed Drops here same as DroppingItems("*Put Item Here*");
        for (int i = 0; i < dropamount; i++)
        {
            tempname = ItDatabase.GetComponent<ItemDatabase>().randomdrop(this.name);
            DroppingItems(tempname); //Dropping of the items
        }
        //Insert code for getting rid of the enemy GameObject and etc. of when he dies
    }

    public void DroppingItems(string name)
    {
        GameObject Item = (GameObject)Instantiate(Resources.Load("Prefab"), transform.position, transform.rotation); //Creates the Object
        if (name == "slime") //Checks if currency or an item was dropped
        {
            Item.GetComponent<ItemDropScript>().SlimeAmount = Random.Range(lowmoney, highmoney); //Where it changes how much currency was dropped
        }
        Item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Item Icons/" + name); //To add an image to the dropped item
        Item.name = name; //Changes the name of the dropped object as to view and used for adding to inventory
    }
}
