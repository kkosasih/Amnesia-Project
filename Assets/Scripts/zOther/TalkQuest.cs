using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : QuestTracking {
    List<string> items;

    public TalkQuest(string newDescription, string newObjective)
    {
        description = newDescription;
        objective = newObjective;
    }

    public void MainQuest1()
    {
        mainquest.Add(new TalkQuest("Find your father.", "You've arrive at the island, but you crashed. Go find you father in the wreckage."));
        items = new List<string>();
        items.Add("vest");
        mainquestloot.Add(items);
    }
}
