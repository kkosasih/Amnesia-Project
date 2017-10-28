using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad {
    public static GameObject player = GameObject.FindWithTag("Player");

    // Saves the needed game info
    public static void Save (int slot)
    {
        string slotNum = slot.ToString();
        if (player != null)
        {
            PlayerPrefs.SetFloat(slotNum + "PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat(slotNum + "PlayerPosY", player.transform.position.y);
            Debug.Log("Player location saved in slot " + slotNum);
        }
    }

    // Loads the needed game info
    public static void Load (int slot)
    {
        string slotNum = slot.ToString();
        if (player != null)
        {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat(slotNum + "PlayerPosX", player.transform.position.x), PlayerPrefs.GetFloat(slotNum + "PlayerPosY", player.transform.position.y), 0);
            Debug.Log("Player location loaded from slot " + slotNum);
        }
    }
}
