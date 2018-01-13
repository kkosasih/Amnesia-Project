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
        
    }
}