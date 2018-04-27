﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTracking {
    #region Attributes
    public static bool innA = true; // Whether the first inn cutscene has played
    public static bool isShipwreckedNotPlayed = true;
	public static bool letsFindAJob = true;
	public static bool bFFindsAJob = true;
	public static bool woodCutterJob = true;
	public static bool enterNeymoursInn = true;
    #endregion

    #region Methods
    // Check for and play a cutscene if needed, returns whether a cutscene played
    public static bool CheckConversation ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7 && innA)
        {
            innA = false;
            DialogueController.instance.ChangeConversation("Inn");
            return true;
        }
		else if (SceneManager.GetActiveScene().buildIndex == 10 && isShipwreckedNotPlayed)
        {
            isShipwreckedNotPlayed = false;
            DialogueController.instance.ChangeConversation("Shipwrecked/Shipwrecked");
            return true;
        }
		else if (SceneManager.GetActiveScene().buildIndex == 10 && bFFindsAJob)
		{
			bFFindsAJob = false;
			DialogueController.instance.ChangeConversation("BFFindsJob/BFFindsJob");
			return true;
		}
		else if (SceneManager.GetActiveScene().buildIndex == 10 && enterNeymoursInn)
		{
			enterNeymoursInn = false;
			DialogueController.instance.ChangeConversation("EnterNeymoursInn/EnterNeymoursInn");
			return true;
		}
		else if (SceneManager.GetActiveScene().buildIndex == 10 && letsFindAJob)
		{
			letsFindAJob = false;
			DialogueController.instance.ChangeConversation("LetsFindaJob/LetsFindaJob");
			return true;
		}
		else if (SceneManager.GetActiveScene().buildIndex == 12 && woodCutterJob)
		{
			woodCutterJob = false;
			DialogueController.instance.ChangeConversation("WoodcutterJob/WoodcutterJob");
			return true;
		}

        return false;
    }

    // Save the progress of each trigger
    public static void SaveTriggers (int slot)
    {
        string slotNum = slot.ToString();
        PlayerPrefs.SetInt(slotNum + "innA", innA ? 1 : 0);
        PlayerPrefs.SetInt(slotNum + "isShipwreckedNotPlayed", isShipwreckedNotPlayed ? 1 : 0);
    }

    // Load the progress of each trigger
    public static void LoadTriggers (int slot)
    {
        string slotNum = slot.ToString();
        innA = PlayerPrefs.GetInt(slotNum + "innA") == 1 ? true : false;
        isShipwreckedNotPlayed =  PlayerPrefs.GetInt(slotNum + "isShipwreckedNotPlayed") == 1 ? true : false;
    }
    #endregion
}
