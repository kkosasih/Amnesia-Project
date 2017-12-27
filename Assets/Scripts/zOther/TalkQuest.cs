using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : QuestTracking {

    public TalkQuest(string newDescription, string newObjective,int mq)
    {
        description = newDescription;
        objective = newObjective;
        mainquestprogress = mq;
    }
}
