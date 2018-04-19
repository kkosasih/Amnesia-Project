using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad {
    #region Attributes
    public static GameObject ccreation = GameObject.Find("Character Creation");
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
            PlayerPrefs.SetInt(slotNum + "Head", ccreation.GetComponent<CharacterLooks>().X);
            PlayerPrefs.SetInt(slotNum + "Cloth", ccreation.GetComponent<CharacterLooks>().Y);
            PlayerPrefs.SetInt(slotNum + "Shoe", ccreation.GetComponent<CharacterLooks>().Z);
            PlayerPrefs.SetInt(slotNum + "Face", ccreation.GetComponent<CharacterLooks>().H);
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
            PlayerCharacter.instance.PlaceOnMap(PlayerPrefs.GetInt(slotNum + "PlayerTile"));
            GameController.instance.SetUpScene(PlayerPrefs.GetInt(slotNum + "SceneNum"));
            PlayerCharacter.instance.ChangeHealth(PlayerPrefs.GetInt(slotNum + "PlayerHealth"));
            PlayerCharacter.instance.ChangeStamina(PlayerPrefs.GetInt(slotNum + "PlayerStamina"));
            //GameObject.Find("Headp"). = PlayerPrefs.GetInt(slotNum + "Head");
            //GameObject.Find("Clothp"). = PlayerPrefs.GetInt(slotNum + "Cloth");
            //GameObject.Find("Shoep"). = PlayerPrefs.GetInt(slotNum + "Shoe");
            //GameObject.Find(""). = PlayerPrefs.GetInt(slotNum + "Face");
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
        }
    }
    #endregion
}
