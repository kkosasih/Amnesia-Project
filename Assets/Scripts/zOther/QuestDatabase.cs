using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    #region Attributes
    static List<string> items;
    static List<string> objectives;
    static List<int> amount;
    static List<int> tamount;
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion

    #region Methods

    public static Quest MainQuest2()
    {
        items = new List<string>();         //List for Loot to be given

        objectives = new List<string>();    //List for what to collect
        objectives.Add("Battlemaster");

        amount = new List<int>();           //Just to fill it in
        amount.Add(1);

        tamount = new List<int>();
        tamount.Add(0);

        return new TalkQuest(
            "Preparing for Battle",
            items,                          //Loot
            objectives,                     //Monsters
            amount,                         //How many Monsters
            tamount,
            0,                              //Which Character is affected
            "Talk to the battlemaster at Base Camp",            //Description
            "",  //Objective
            true,
            false);
    }

    public static Quest MainQuest1()
    {
        items = new List<string>();         //List for Loot to be given

        objectives = new List<string>();    //List for what creatures need to be killed
        objectives.Add("Innkeeper");

        amount = new List<int>();       //List for how many of that creature to kill
        amount.Add(1);

        tamount = new List<int>();
        tamount.Add(0);

        return new TalkQuest(
            "Defend Yourself",
            items,                          //Loot
            objectives,                     //Monsters
            amount,                         //How many Monsters
            tamount,
            1,                              //Which Character is affected
            "Talk to the innkeeper.", //Description
            "",  //Objective
            true,                           
            false);                             
    }
    #endregion
}