using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracking : MonoBehaviour
{
    public List<QuestTypes> availablequests;
    public List<QuestTypes> mainList;
    public List<QuestTypes> questList;
    public int[,] character;
    public GameObject Inventory;
    public GameObject Speaker;
    public GameObject FinUI;
    public Text Title;
    public Text speaker;
    public int questinc = 0;

    void Start()
    {
        mainList = new List<QuestTypes>();
        questList = new List<QuestTypes>();
        character = new int[10, 2];
        mainList.Add(QuestDatabase.MainQuest1());
        mainList.Add(QuestDatabase.MainQuest2());
        //questList.Add(QuestDatabase.MainQuest1());
    }

    // Update is called once per frame
    void Update()
    {
        mainList[questinc].updatedprog();
        if (mainList[questinc].finished == true)
       {
            FinishQuest(questinc);// May want to add something that waits until the ui has disappeared then brings it back
        }        

        /*for (int z = 0; z < questList.Count; z++)
        {
            if (questList[z].finished == false)
            {
                questList[z].updatedprog();
                if (questList[z].finished == true)
                {
                    FinishQuest(z);
                }
            }
        }*/
    }

    public void FinishQuest(int x)
    {
        FinUI.SetActive(true);
        if (mainList[x].mainquest == true)
        {
            Title.text = mainList[x].questname;
            for (int y = 0; y < mainList[x].questListloot.Count; y++)
            {
                Inventory.GetComponent<Inventory>().AddItem(mainList[x].questListloot[y]);
            }
            character[mainList[x].character,0] += 1;
            character[mainList[x].character, mainList[x].characterprog] += 1;
            questinc += 1;
        }
        else
        {
            Title.text  = questList[x].questname;
            for (int y = 0; y < questList[x].questListloot.Count; y++)
            {
                Inventory.GetComponent<Inventory>().AddItem(questList[x].questListloot[y]);
            }
            character[questList[x].character, 0] += 1;
            character[questList[x].character, questList[x].characterprog] += 1;
        }
    }

    public void questobj(string target)
    {
        mainList[questinc].obj(target);
        for (int z = 0; z < questList.Count; z++)
        {
            if (questList[z].finished == false)
            {
                questList[z].obj(target);
            }
        }
    }

    public void speakobj()
    {
        // Something checking you wanted to turn in the quest (put here)
        questobj(speaker.text);
    }
}
