using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad {
    public static GameObject player = GameObject.FindWithTag("Player");
    public static GameObject ccreation = GameObject.Find("Character Creation");

    // Saves the needed game info
    public static void Save (int slot)
    {
        string slotNum = slot.ToString();
        // Save player data
        if (player != null)
        {
            PlayerPrefs.SetInt(slotNum + "SceneNum", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(slotNum + "PlayerTile", player.GetComponent<PlayerCharacter>().currentTile);
            PlayerPrefs.SetInt(slotNum + "PlayerHealth", player.GetComponent<PlayerCharacter>().health);
            PlayerPrefs.SetInt(slotNum + "PlayerStamina", player.GetComponent<PlayerCharacter>().stamina);
            PlayerPrefs.SetString(slotNum + "Head", ccreation.GetComponent<CharacterLooks>().headc);
            PlayerPrefs.SetString(slotNum + "Cloth", ccreation.GetComponent<CharacterLooks>().clothec);
            PlayerPrefs.SetString(slotNum + "Shoe", ccreation.GetComponent<CharacterLooks>().shoec);
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
            player.GetComponent<PlayerCharacter>().currentTile = PlayerPrefs.GetInt(slotNum + "PlayerTile");
            GameController.SetUpScene(PlayerPrefs.GetInt(slotNum + "SceneNum"));
            player.GetComponent<PlayerCharacter>().ChangeHealth(PlayerPrefs.GetInt(slotNum + "PlayerHealth"));
            player.GetComponent<PlayerCharacter>().ChangeStamina(PlayerPrefs.GetInt(slotNum + "PlayerStamina"));
            //GameObject.Find("Headp").GetComponent<Image>().sprite = Resources.Load<Sprite>("CharacterTemp/" + PlayerPrefs.GetInt(slotNum + "Head"));
            //GameObject.Find("Clothp").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CharacterTemp/" + PlayerPrefs.GetInt(slotNum + "Cloth"));
            //GameObject.Find("Shoep").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("CharacterTemp/" + PlayerPrefs.GetInt(slotNum + "Shoe"));
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
}
