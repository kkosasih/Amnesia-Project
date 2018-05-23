using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTracking {
    #region Attributes
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
		if (SceneManager.GetActiveScene().name == "Beach1" && isShipwreckedNotPlayed)
        {
            isShipwreckedNotPlayed = false;
            DialogueController.instance.ChangeConversation("Shipwrecked/Shipwrecked");
            return true;
        }
        else if (!isShipwreckedNotPlayed && SceneManager.GetActiveScene().name == "Interior5" && enterNeymoursInn)
        {
            enterNeymoursInn = false;
            DialogueController.instance.ChangeConversation("EnterNeymoursInn/EnterNeymoursInn");
            return true;
        }
        else if (!enterNeymoursInn && SceneManager.GetActiveScene().name == "Interior6" && letsFindAJob)
        {
            letsFindAJob = false;
            DialogueController.instance.ChangeConversation("LetsFindaJob/LetsFindaJob");
            return true;
        }
        else if (!letsFindAJob && SceneManager.GetActiveScene().name == "Interior6" && bFFindsAJob)
		{
			bFFindsAJob = false;
			DialogueController.instance.ChangeConversation("BFFindsJob/BFFindsJob");
			return true;
		}
		else if (!bFFindsAJob && SceneManager.GetActiveScene().name == "Interior12" && woodCutterJob)
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
        PlayerPrefs.SetInt(slotNum + "isShipwreckedNotPlayed", isShipwreckedNotPlayed ? 1 : 0);
    }

    // Load the progress of each trigger
    public static void LoadTriggers (int slot)
    {
        string slotNum = slot.ToString();
        isShipwreckedNotPlayed =  PlayerPrefs.GetInt(slotNum + "isShipwreckedNotPlayed") == 1 ? true : false;
    }
    #endregion
}
