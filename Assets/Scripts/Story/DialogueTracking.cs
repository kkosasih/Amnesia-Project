using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTracking {
    public static bool innA = true;

    // Check for and play a cutscene if needed, returns whether a cutscene played
    public static bool CheckConversation ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 && innA)
        {
            innA = false;
            DialogueController.instance.ChangeConversation("Inn");
            return true;
        }
        return false;
    }
}
