using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance {
    public static void TeleportPlayer()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Gameplay"))
        {
            SceneManager.LoadScene("Interior");
            GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().MovePlayer(17);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Interior"))
        {
            SceneManager.LoadScene("Gameplay");
            GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().MovePlayer(7);
        }
    }
}
