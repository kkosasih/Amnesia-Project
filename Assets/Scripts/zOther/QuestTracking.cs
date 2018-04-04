using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracking : MonoBehaviour
{
    public List<QuestTypes> availablequests;
    public List<QuestTypes> mainList;
    public List<QuestTypes> questList;
    public List<QuestTypes> fquestList;
    public int[,] character;
    public GameObject Inventory;
    public GameObject Speaker;
    public GameObject FinUI;
    public CanvasGroup FinUI2;
    public bool finuiup = false;
    public Text Title;
    public Text speaker;
    public Text stfin;
    public string questname;
    public int questinc = 0, questinc2 = 0;
    public float num = 0;

    void Start()
    {
        mainList = new List<QuestTypes>();
        questList = new List<QuestTypes>();
        fquestList = new List<QuestTypes>();
        character = new int[10, 2];
        
        mainList.Add(QuestDatabase.MainQuest2());
        mainList.Add(QuestDatabase.MainQuest1());
        //questList.Add(QuestDatabase.MainQuest1());
        addingquests("GeneralQuest1");

        FinUI2 = FinUI.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(speaker.text != "Name")
        {
            speakobj();
        }

        //For getting the started quest menu
        if(questinc < mainList.Count && ((questinc2 - 1) != questinc || (questinc == 0 && questinc2 == 0)) && (finuiup == false))
        {
            stfin.text = "Quest Start";
            FinUI2.alpha = 0;
            FinUI.SetActive(true);
            finuiup = true;
            Title.text = mainList[questinc].questname;
            StartCoroutine(Fade(FinUI2));
            questinc2 += 1;
        }

        for (int z = 0; z < questList.Count; z++)
        {
            if (finuiup == false && questList[z] != null && questList[z].mainquest != true)
            {
                stfin.text = "Quest Start";
                FinUI2.alpha = 0;
                finuiup = true;
                FinUI.SetActive(true);
                Title.text = questList[z].questname;
                questList[z].mainquest = true;
                StartCoroutine(Fade(FinUI2));
            }
        }

        //For Keeping track of the progress of the menu
        if (questinc < mainList.Count)
        {
            mainList[questinc].updatedprog();
            if (mainList[questinc].finished == true && finuiup == false)
            {
                finuiup = true;
                FinishQuest(questinc);
            }
        }

        //Make sure there isn't any big delay for any of the processes...

        for (int z = 0; z < questList.Count; z++)
        {
            questList[z].updatedprog();
            if (questList[z].finished == true && finuiup == false && questList[z] != null)
            {
                finuiup = true;
                fquestList.Add(questList[z]);
                FinishQuest(z);
                questList.Remove(questList[z]);
            }
        }
    }

    public void FinishQuest(int x)
    {
        FinUI2.alpha = 0;       
        stfin.text = "Finished!!!";
        if (mainList[x].finished == true)
        {
            Title.text = mainList[x].questname;
            for (int y = 0; y < mainList[x].questListloot.Count; y++)
            {
                //Inventory.GetComponent<Inventory>().AddItem(mainList[x].questListloot[y]);
            }
            fquestList.Add(mainList[x]);
            character[mainList[x].character,0] += 1;
            character[mainList[x].character, mainList[x].characterprog] += 1;
            questinc += 1;
        }
        else
        {
            Title.text  = questList[x].questname;
            for (int y = 0; y < questList[x].questListloot.Count; y++)
            {
                //Inventory.GetComponent<Inventory>().AddItem(questList[x].questListloot[y]);
            }
            character[questList[x].character, 0] += 1;
            character[questList[x].character, questList[x].characterprog] += 1;
        }
        FinUI.SetActive(true);
        StartCoroutine(Fade(FinUI2));
    }

    public void questobj(string target)
    {
        if (questinc < mainList.Count)
        {
            mainList[questinc].obj(target);
        }
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
        // Something checking who you wanted to turn in the quest to (put here)
        questobj(speaker.text);
    }

    public IEnumerator Fade(CanvasGroup ui)
    {
        while (true)
        {
            ui.alpha += 0.05f;

            if (ui.alpha >= 1)
            {
                break;
            }

            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(2.0f);
        while(true)
        {
            ui.alpha -= 0.05f;

            if(ui.alpha <= 0)
            {
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }
        FinUI.SetActive(false);
        finuiup = false;
    }

    public void addingquests(string qname)
    {
        questList.Add(QuestDatabase.Fix(qname));
    }
}
