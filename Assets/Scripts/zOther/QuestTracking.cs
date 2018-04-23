using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracking : MonoBehaviour
{
    #region Attributes
    public static QuestTracking instance;
    public List<QuestTypes> availablequests;
    public List<QuestTypes> mainList;
    public List<QuestTypes> questList;
    public List<QuestTypes> fquestList;
    public int[,] character;
    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private GameObject Speaker;
    [SerializeField]
    private GameObject FinUI;
    [SerializeField]
    private CanvasGroup FinUI2;
    [SerializeField]
    private Text Title;
    [SerializeField]
    private Text speaker;
    [SerializeField]
    private Text stfin;
    private bool finuiup = false;
    private string questname;
    private int questinc = 0, questinc2 = 0;
    #endregion

    #region Properties
    // Returns questinc
    public int Questinc
    {
        get
        {
            return questinc;
        }
        set
        {
            questinc = value;
        }
    }

    // Returns questinc2
    public int Questinc2
    {
        get
        {
            return questinc2;
        }
        set
        {
            questinc2 = value;
        }
    }
    #endregion

    #region Event Functions
    void Awake ()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

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

    // Use this for initialization
    void Start ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speaker.text != "Name")
        {
            speakobj();
        }

        //For getting the started quest menu
        if (questinc < mainList.Count && ((questinc2 - 1) != questinc || (questinc == 0 && questinc2 == 0)) && (finuiup == false))
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
    #endregion

    #region Methods
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

    public void addingquests(string qname)
    {
        questList.Add(QuestDatabase.Fix(qname));
    }

    // Save quest data
    public void SaveQuests (int slot)
    {
        string slotNum = slot.ToString();
        PlayerPrefs.SetInt(slotNum + "questinc", Questinc);
        PlayerPrefs.SetInt(slotNum + "questinc2", Questinc2);
        foreach (QuestTypes q in mainList)
        {
            PlayerPrefs.SetInt(slotNum + q.questname, q.finished ? 1 : 0);
        }
        foreach (QuestTypes q in questList)
        {
            PlayerPrefs.SetInt(slotNum + q.questname, q.finished ? 1 : 0);
        }
        foreach (QuestTypes q in fquestList)
        {
            PlayerPrefs.SetInt(slotNum + q.questname, q.finished ? 1 : 0);
        }
    }

    // Save quest data
    public void LoadQuests (int slot)
    {
        string slotNum = slot.ToString();
        Questinc = PlayerPrefs.GetInt(slotNum + "questinc");
        Questinc2 = PlayerPrefs.GetInt(slotNum + "questinc2");
        foreach (QuestTypes q in mainList)
        {
            q.finished = PlayerPrefs.GetInt(slotNum + q.questname) == 1 ? true : false;
        }
        foreach (QuestTypes q in questList)
        {
            q.finished = PlayerPrefs.GetInt(slotNum + q.questname) == 1 ? true : false;
        }
        foreach (QuestTypes q in fquestList)
        {
            q.finished = PlayerPrefs.GetInt(slotNum + q.questname) == 1 ? true : false;
        }
    }
    #endregion

    #region Coroutines
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
    #endregion
}
