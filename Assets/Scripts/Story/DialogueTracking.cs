using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTracking {
    #region Attributes
    public static bool innA = true; // Whether the first inn cutscene has played
    public static bool isShipwreckedNotPlayed = true;
    #endregion

    #region Methods
    // Check for and play a cutscene if needed, returns whether a cutscene played
    public static bool CheckConversation ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7 && innA)
        {
            innA = false;
            DialogueController.instance.ChangeConversation("Inn");
            return true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 12 && isShipwreckedNotPlayed)
        {
            isShipwreckedNotPlayed = false;
            DialogueController.instance.ChangeConversation("Shipwrecked/Shipwrecked");
            return true;
        }
        return false;
    }
    #endregion
}
