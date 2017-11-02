using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad {
    public static GameObject player = GameObject.FindWithTag("Player");
    public static GameObject map = GameObject.FindWithTag("Map");

    // Saves the needed game info
    public static void Save (int slot)
    {
        string slotNum = slot.ToString();
        if (player != null)
        {
            PlayerPrefs.SetString(slotNum + "SceneNum", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetInt(slotNum + "PlayerTile", GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().currentTile);
            PlayerPrefs.SetInt(slotNum + "PlayerHealth", player.GetComponent<PlayerStats>().health);
            PlayerPrefs.SetInt(slotNum + "PlayerStamina", player.GetComponent<PlayerStats>().stamina);
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
        if (player != null)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString(slotNum + "SceneNum"));
            GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().MovePlayer(PlayerPrefs.GetInt(slotNum + "PlayerTile"));
            player.GetComponent<PlayerStats>().ChangeHealth(PlayerPrefs.GetInt(slotNum + "PlayerHealth"));
            player.GetComponent<PlayerStats>().ChangeStamina(PlayerPrefs.GetInt(slotNum + "PlayerStamina"));
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
