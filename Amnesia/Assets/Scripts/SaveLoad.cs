using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad {
    public static GameObject player = GameObject.FindWithTag("Player");

    // Saves the needed game info
    public static void Save ()
    {
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
            Debug.Log("Player location saved");
        }
    }

    // Loads the needed game info
    public static void Load ()
    {
        if (player != null)
        {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPosX", player.transform.position.x), PlayerPrefs.GetFloat("PlayerPosY", player.transform.position.y), 0);
            Debug.Log("Player location loaded");
        }
    }
}
