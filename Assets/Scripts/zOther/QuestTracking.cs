using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracking : MonoBehaviour
{
    public List<QuestTracking> mainquest;
    public List<List<string>> mainquestloot;
    public List<QuestTracking> questList;
    public List<List<string>> questListloot;
    public List<List<string>> killncollectquest;
    public List<QuestTracking> fquests;
    public int[,] character;
    public GameObject Inventory;
    public string description;
    public string objective;
    public Text mainquestdescription;
    public Text mainquestobjective;
    public int questinc = 0;

    void Start()
    {
        //Keeps track of the main quest and loot rewards
        mainquest = new List<QuestTracking>();
        mainquestloot = new List<List<string>>();
        //Keeps track of general quests
        questList = new List<QuestTracking>();
        questListloot = new List<List<string>>();
        //Keeps track of what is suppose to be kill and/or collected also people/person to be talked to
        killncollectquest = new List<List<string>>();
        //A list that keeps track of what lists have been completed
        fquests = new List<QuestTracking>();
        //Keeps track of progress and relationship with characters
        character = new int[9, 3]; //Note the array size should change from nine depending on how many characters
        //Adds the main quests to the dynamic array (Since main quest is linear may make a set array...Also reason why is added straight)
        Inventory.GetComponent<TalkQuest>().MainQuest1();
        Inventory.GetComponent<KillQuest>().MainQuest2();
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
        for (int y = 0; y < 999; y++)
        {
            Inventory.GetComponent<Inventory>().AddItem(questListloot[x][y]);
        }
    }

    public void FinishMainQuest()
    {
        for (int y = 0; y < 999; y++)
        {
            Inventory.GetComponent<Inventory>().AddItem(mainquestloot[questinc][y]);
        }
        questinc += 1;
    }
}