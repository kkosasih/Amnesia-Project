using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{

    static List<string> items;
    static List<string> objectives;
    static List<int> amount;
    static List<int> tamount;
    

    public static QuestTypes Fix(string name)
    {
        switch(name)
        {
            case "GeneralQuest1":
                return GeneralQuest1();
        }
        return MainQuest1();
    }

    public static QuestTypes MainQuest1()
    {
        items = new List<string>();         //List for Loot to be given
        items.Add("vest");

        objectives = new List<string>();    //List for who to talk to
        objectives.Add("Wood");

        amount = new List<int>();           //Just to fill it in
        amount.Add(5);

        tamount = new List<int>();
        tamount.Add(0);

        return new CollectQuest(
            "Collecting Lumber",
            items,                          //Loot
            objectives,                     //Monsters
            amount,                         //How many Monsters
            tamount,
            0,                              //Which Character is affected
            "The innkeeper gave you a chore to repay your stay",            //Description
            "Collect wood by chopping down a tree",  //Objective
            true,
            false);
    }

    public static QuestTypes MainQuest2()
    {
        items = new List<string>();         //List for Loot to be given
        items.Add("vest");

        objectives = new List<string>();    //List for what creatures need to be killed
        objectives.Add("Monster");

        amount = new List<int>();       //List for how many of that creature to kill
        amount.Add(1);

        tamount = new List<int>();
        tamount.Add(0);

        return new KillQuest(
            "Defend Yourself",
            items,                          //Loot
            objectives,                     //Monsters
            amount,                         //How many Monsters
            tamount,
            1,                              //Which Character is affected
            "Defend against the monsters.", //Description
            "Monsters are attacking you.",  //Objective
            true,                           
            false);                             
    }

    public static QuestTypes GeneralQuest1()
    {
        items = new List<string>();         //List for Loot to be given
        items.Add("vest");

        objectives = new List<string>();    //List for who to talk to
        objectives.Add("Sign");

        amount = new List<int>();           //Just to fill it in
        amount.Add(1);

        tamount = new List<int>();
        tamount.Add(0);

        return new TalkQuest(
            "Learning to Talk",
            items,                          //Loot
            objectives,                     //Monsters
            amount,                         //How many Monsters
            tamount,
            0,                              //Which Character is affected
            "Find your father.",            //Description
            "You've arrive at the island, but you crashed. Go find you father in the wreckage.",  //Objective
            false,
            false);
    }
}