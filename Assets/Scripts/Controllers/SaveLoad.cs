using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad {
    #region Attributes

    #endregion

    #region Methods
    // Saves the needed game info
    public static void Save (int slot)
    {
        string slotNum = slot.ToString();
        // Save player data
        if (PlayerCharacter.instance != null)
        {
            PlayerPrefs.SetInt(slotNum + "SceneNum", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(slotNum + "PlayerTile", PlayerCharacter.instance.CurrentTile);
            PlayerPrefs.SetInt(slotNum + "PlayerHealth", PlayerCharacter.instance.health);
            PlayerPrefs.SetInt(slotNum + "PlayerStamina", PlayerCharacter.instance.stamina);
            PlayerPrefs.SetInt(slotNum + "Head", CharacterLooks.instance.X);
            PlayerPrefs.SetInt(slotNum + "Cloth", CharacterLooks.instance.Y);
            PlayerPrefs.SetInt(slotNum + "Shoe", CharacterLooks.instance.Z);
            PlayerPrefs.SetInt(slotNum + "Face", CharacterLooks.instance.H);
            DialogueTracking.SaveTriggers(slot);
            QuestTracking.instance.SaveQuests(slot);
            Inventory inven = GameObject.Find("Inventory").GetComponent<Inventory>();
            for (int i = 0; i < inven.Items.Count; ++i)
            {
                if (inven.Items[i].itemName != null)
                {
                    PlayerPrefs.SetInt(slotNum + "Inventory" + i.ToString(), inven.Items[i].itemId);
                }
                else
                {
                    PlayerPrefs.SetInt(slotNum + "Inventory" + i.ToString(), -1);
                }
            }
        }
    }

    // Loads the needed game info
    public static void Load (int slot)
    {
        string slotNum = slot.ToString();
        // Set scene for loading
        GameObject.Find("ShopWindow").GetComponent<UIPanel>().IsOpen = false;
        GameObject.Find("DialogueBox").GetComponent<UIPanel>().IsOpen = false;
        // Load player data
        if (PlayerCharacter.instance != null)
        {
            PlayerCharacter.instance.startTile = PlayerPrefs.GetInt(slotNum + "PlayerTile");
            PlayerCharacter.instance.ChangeHealth(PlayerPrefs.GetInt(slotNum + "PlayerHealth"));
            PlayerCharacter.instance.ChangeStamina(PlayerPrefs.GetInt(slotNum + "PlayerStamina"));
            int _x = PlayerPrefs.GetInt(slotNum + "Head");
            int _y = PlayerPrefs.GetInt(slotNum + "Cloth");
            int _z = PlayerPrefs.GetInt(slotNum + "Shoe");
            int _h = PlayerPrefs.GetInt(slotNum + "Face");
            CharacterLooks.instance.Set(_x, _y, _z, _h);
            DialogueTracking.LoadTriggers(slot);
            QuestTracking.instance.LoadQuests(slot);
            Inventory inven = GameObject.Find("Inventory").GetComponent<Inventory>();
            inven.Clear();
            for (int i = 0; i < inven.Items.Count; ++i)
            {
                int idToAdd = PlayerPrefs.GetInt(slotNum + "Inventory" + i.ToString());
                if (idToAdd != -1)
                {
                    inven.Items[i] = GameController.instance.Database.items[idToAdd];
                }
            }
            GameController.instance.SetUpSceneFunc(PlayerPrefs.GetInt(slotNum + "SceneNum"));
        }
    }
    #endregion
}
