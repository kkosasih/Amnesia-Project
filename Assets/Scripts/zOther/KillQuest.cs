using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : QuestTracking {

    private int killamount = 0;

    public KillQuest(string newDescription, string newObjective, int mq, int ka)
    {
        description = newDescription;
        objective = newObjective;
        mainquestprogress = mq;
    }
}
