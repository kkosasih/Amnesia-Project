using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracking
{
    public List<QuestTracking> mainquest = new List<QuestTracking>();
    public List<QuestTracking> questList = new List<QuestTracking>();
    public List<QuestTracking> fquests = new List<QuestTracking>();
    public GameObject Inventory;
    public string description;
    public string objective;
    public Text mainquestdescription;
    public Text mainquestobjective;
    public int questinc = 0;
    public int mainquestprogress = 0;
    public int charactera = 0, characterb = 0;

    void Start()
    {
        mainquest.Add(new TalkQuest("Find your father.", "You've arrive at the island, but you crashed. Go find you father in the wreckage.", 1));
        /*More MainQuests*/
        questList.Add(null);
        fquests.Add(null);
    }

    // Update is called once per frame
    void Update()
    {
        mainquestdescription.text = mainquest[questinc].description;
        mainquestobjective.text = mainquest[questinc].objective;
        /********************************************************************************/
        /*if()
        {
            questList.Add(new );
        }

        if()
        {

        }*/
    }

    public void FinishQuest()
    {
        questinc += mainquestprogress;
        Inventory.AddItem();
    }
}
