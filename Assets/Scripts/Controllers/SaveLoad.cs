using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad {
    #region Attributes
    public static GameObject player = GameObject.FindWithTag("Player");
    public static GameObject ccreation = GameObject.Find("Character Creation");
    #endregion

    #region Methods
    // Saves the needed game info
    public static void Save (int slot)
    {
        string slotNum = slot.ToString();
        // Save player data
        if (player != null)
        {
            PlayerPrefs.SetInt(slotNum + "SceneNum", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(slotNum + "PlayerTile", GameController.map.takenTiles[PlayerCharacter.instance]);
            PlayerPrefs.SetInt(slotNum + "PlayerHealth", player.GetComponent<PlayerCharacter>().health);
            PlayerPrefs.SetInt(slotNum + "PlayerStamina", player.GetComponent<PlayerCharacter>().stamina);
            PlayerPrefs.SetInt(slotNum + "Head", ccreation.GetComponent<CharacterLooks>().x);
            PlayerPrefs.SetInt(slotNum + "Cloth", ccreation.GetComponent<CharacterLooks>().y);
            PlayerPrefs.SetInt(slotNum + "Shoe", ccreation.GetComponent<CharacterLooks>().z);
            PlayerPrefs.SetInt(slotNum + "Face", ccreation.GetComponent<CharacterLooks>().h);
            Inventory inven = GameObject.Find("Inventory").GetComponent<Inventory>();
            for (int i = 0; i < inven.inventory.Count; ++i)
            {
                if (inven.inventory[i].itemName != null)
                {
                    PlayerPrefs.SetInt(slotNum + "Inventory" + i.ToString(), inven.inventory[i].itemId);
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
        GameObject.Find("ShopWindow").GetComponent<UIPanel>().isOpen = false;
        GameObject.Find("DialogueBox").GetComponent<UIPanel>().isOpen = false;
        // Load player data
        if (player != null)
        {
            GameController.map.takenTiles[PlayerCharacter.instance] = PlayerPrefs.GetInt(slotNum + "PlayerTile");
            GameController.SetUpScene(PlayerPrefs.GetInt(slotNum + "SceneNum"));
            player.GetComponent<PlayerCharacter>().ChangeHealth(PlayerPrefs.GetInt(slotNum + "PlayerHealth"));
            player.GetComponent<PlayerCharacter>().ChangeStamina(PlayerPrefs.GetInt(slotNum + "PlayerStamina"));
            //GameObject.Find("Headp"). = PlayerPrefs.GetInt(slotNum + "Head");
            //GameObject.Find("Clothp"). = PlayerPrefs.GetInt(slotNum + "Cloth");
            //GameObject.Find("Shoep"). = PlayerPrefs.GetInt(slotNum + "Shoe");
            //GameObject.Find(""). = PlayerPrefs.GetInt(slotNum + "Face");
            Inventory inven = GameObject.Find("Inventory").GetComponent<Inventory>();
            inven.Clear();
            for (int i = 0; i < inven.inventory.Count; ++i)
            {
                int idToAdd = PlayerPrefs.GetInt(slotNum + "Inventory" + i.ToString());
                if (idToAdd != -1)
                {
                    inven.inventory[i] = ItemDatabase.items[idToAdd];
                }
            }
        }
    }
    #endregion
}
