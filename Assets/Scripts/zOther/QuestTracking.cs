using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracking : MonoBehaviour
{
    public List<QuestTracking> mainquest = new List<QuestTracking>();
    public List<List<string>> mainquestloot = new List<List<string>>();
    public List<QuestTracking> questList = new List<QuestTracking>();
    public List<List<string>> questListloot = new List<List<string>>();
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
        mainquestloot = new List<List<string>>();
        questListloot = new List<List<string>>();
        Inventory.GetComponent<TalkQuest>().MainQuest1();
        //mainquest.Add(new KillQuest("Defend against the monsters.", "Monsters are attacking you.", 5));
        /*More MainQuests*/
    }

    // Update is called once per frame
    void Update()
    {
        mainquestdescription.text = mainquest[questinc].description;
        mainquestobjective.text = mainquest[questinc].objective;
        /********************************************************************************/
        /*if()
        {
            
        }

        if()
        {

        }*/
        /********************************************************************************/
        /*if()
        {
            questList.Add(new );
        }

        if()
        {

        }*/
    }

    public void FinishQuest(int x)
    {
        for(int y = 0; y < 999; y++)
        {
            Inventory.GetComponent<Inventory>().AddItem(questListloot[x][y]);
        }
    }

    public void FinishMainQuest()
    {
        for(int y = 0; y < 999; y++)
        {
            Inventory.GetComponent<Inventory>().AddItem(mainquestloot[questinc][y]);
        }
        questinc += 1;
    }
}