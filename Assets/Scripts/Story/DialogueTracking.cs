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
	public static bool gluttonyAfterFight = true;
	public static bool gluttonyAidenDefeated = true;
	public static bool gluttonyAidenOgre = true;
	public static bool gluttonyDream3 = true;
	public static bool gluttonyEnterRenee = true;
	public static bool gluttonyForestEntrance = true;
	public static bool gluttonyTheLostKids = true;
	public static bool greedAnaDefeated = true;
	public static bool greedAnaMissing = true;
	public static bool greedAnaMonster = true;
	public static bool greedDream5 = true;
	public static bool envyAfterSewer = true;
	public static bool envyFoundZaine = true;
	public static bool envyWhere_sZaine = true;
	public static bool envyZaineDefeated = true;
	public static bool slothBacktoInn = true;
	public static bool slothDream1 = true;
	public static bool slothSherriDefeated = true;
	public static bool slothSherriLost = true;
	public static bool slothSherriMonster = true;
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
		else if (SceneManager.GetActiveScene().name == "Interior1" && gluttonyAfterFight)
		{
			gluttonyAfterFight = false;
			DialogueController.instance.ChangeConversation("Gluttony/AfterFight");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && gluttonyAidenDefeated)
		{
			gluttonyAidenDefeated = false;
			DialogueController.instance.ChangeConversation("Gluttony/AidenDefeated");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && gluttonyAidenOgre)
		{
			gluttonyAidenOgre = false;
			DialogueController.instance.ChangeConversation("Gluttony/AidenOgre");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && gluttonyDream3)
		{
			gluttonyDream3 = false;
			DialogueController.instance.ChangeConversation("Gluttony/Dream3");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && gluttonyEnterRenee)
		{
			gluttonyEnterRenee = false;
			DialogueController.instance.ChangeConversation("Gluttony/EnterRenee");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && gluttonyForestEntrance)
		{
			gluttonyForestEntrance = false;
			DialogueController.instance.ChangeConversation("Gluttony/ForestEntrance");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && gluttonyTheLostKids)
		{
			//gluttonyTheLostKids = false;
			DialogueController.instance.ChangeConversation("Gluttony/TheLostKids");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && envyAfterSewer)
		{
			envyAfterSewer = false;
			DialogueController.instance.ChangeConversation("Envy/AfterSewer");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && envyFoundZaine)
		{
			envyFoundZaine = false;
			DialogueController.instance.ChangeConversation("Envy/FoundZaine");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && envyWhere_sZaine)
		{
			envyWhere_sZaine = false;
			DialogueController.instance.ChangeConversation("Envy/Where_sZaine");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && envyZaineDefeated)
		{
			envyZaineDefeated = false;
			DialogueController.instance.ChangeConversation("Envy/ZaineDefeated");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && greedAnaDefeated)
		{
			greedAnaDefeated = false;
			DialogueController.instance.ChangeConversation("Greed/AnaDefeated");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && greedAnaMissing)
		{
			greedAnaMissing = false;
			DialogueController.instance.ChangeConversation("Greed/AnaMissing");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && greedAnaMonster)
		{
			greedAnaMonster = false;
			DialogueController.instance.ChangeConversation("Greed/AnaMonster");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && greedDream5)
		{
			greedDream5 = false;
			DialogueController.instance.ChangeConversation("Greed/Dream5");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && slothBacktoInn)
		{
			slothBacktoInn = false;
			DialogueController.instance.ChangeConversation("Sloth/BacktoInn");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && slothDream1)
		{
			slothDream1 = false;
			DialogueController.instance.ChangeConversation("Sloth/Dream1");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && slothSherriDefeated)
		{
			slothSherriDefeated = false;
			DialogueController.instance.ChangeConversation("Sloth/SherriDefeated");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && slothSherriLost)
		{
			slothSherriLost = false;
			DialogueController.instance.ChangeConversation("Sloth/SherriLost");
			return true;
		}
		else if (SceneManager.GetActiveScene().name == "Interior1" && slothSherriMonster)
		{
			slothSherriMonster = false;
			DialogueController.instance.ChangeConversation("Sloth/SherriMonster");
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
