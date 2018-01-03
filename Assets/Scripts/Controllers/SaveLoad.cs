using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad {
    public static GameObject player = GameObject.FindWithTag("Player");

    // Saves the needed game info
    public static void Save (int slot)
    {
        string slotNum = slot.ToString();
        if (player != null)
        {
            PlayerPrefs.SetInt(slotNum + "SceneNum", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(slotNum + "PlayerTile", player.GetComponent<PlayerCharacter>().currentTile);
            PlayerPrefs.SetInt(slotNum + "PlayerHealth", player.GetComponent<PlayerCharacter>().health);
            PlayerPrefs.SetInt(slotNum + "PlayerStamina", player.GetComponent<PlayerCharacter>().stamina);
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
        GameObject.Find("ShopWindow").GetComponent<UIPanel>().isOpen = false;
        GameObject.Find("DialogueBox").GetComponent<UIPanel>().isOpen = false;
        if (player != null)
        {
            player.GetComponent<PlayerCharacter>().currentTile = PlayerPrefs.GetInt(slotNum + "PlayerTile");
            GameController.SetUpScene(PlayerPrefs.GetInt(slotNum + "SceneNum"));
            player.GetComponent<PlayerCharacter>().ChangeHealth(PlayerPrefs.GetInt(slotNum + "PlayerHealth"));
            player.GetComponent<PlayerCharacter>().ChangeStamina(PlayerPrefs.GetInt(slotNum + "PlayerStamina"));
            Inventory inven = GameObject.Find("Inventory").GetComponent<Inventory>();
            inven.Clear();
            for (int i = 0; i < inven.inventory.Count; ++i)
            {
                int idToAdd = PlayerPrefs.GetInt(slotNum + "Inventory" + i.ToString());
                if (idToAdd != -1)
                {
                    inven.inventory[i] = GameObject.Find("Item Database").GetComponent<ItemDatabase>().items[idToAdd];
                }
            }
        }
    }
}
