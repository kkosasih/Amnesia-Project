using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : QuestTypes
{
    #region Constructors
    public TalkQuest(string newqn, List<string> newListloot, List<string> newobjectives, List<int> ka, List<int> kta, int newcharacter, string newDescription, string newObjective, bool main, bool fin)
    {
        questname = newqn;
        questListloot = newListloot;
        kncquest = newobjectives;
        kncamount = ka;
        knctotalamount = kta;
        character = newcharacter;
        description = newDescription;
        objective = newObjective;
        mainquest = main;
        finished = fin;
    }
    #endregion
}