using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectQuest : QuestTracking
{
    List<string> items;
    private int collectamount = 0;
    List<string> objectives;

    public CollectQuest(string newDescription, string newObjective, int ca)
    {
        description = newDescription;
        objective = newObjective;
        collectamount = ca;
    }
}
