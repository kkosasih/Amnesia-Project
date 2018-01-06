using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTypes
{
    public string questname;
    public List<string> questListloot;
    public List<string> kncquest;
    public List<int> kncamount;
    public List<int> knctotalamount;
    public int character;
    public int characterprog = 1; // If 1 then increases character path a else 2 for path b
    public string description;
    public string objective;
    public bool mainquest;
    public bool finished;

    public void updatedprog()
    {
        for (int i = 0; i < kncamount.Count; i++)
        {
            if(kncamount[i] > knctotalamount[i])
            {
                return;
            }
        }
        finished = true;
    }

    public void obj(string target)
    {
        for(int i = 0; i < kncamount.Count; i++)
        {
            if(kncquest[i] == target)
            {
                knctotalamount[i] += 1;
                return;
            }
        }
    }
}
