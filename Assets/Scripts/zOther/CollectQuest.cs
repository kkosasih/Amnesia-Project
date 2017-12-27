using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectQuest : QuestTracking {

    private int collectamount = 0;

    public CollectQuest(string newDescription, string newObjective, int mq, int ca)
    {
        description = newDescription;
        objective = newObjective;
        mainquestprogress = mq;
        collectamount = ca;
    }
}
