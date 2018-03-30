using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : StaticObject {
    public string path; // The name of the text file to reference

    void Awake ()
    {
        GetComponent<Interactible>().interact = ReadSign;
    }

    // Read the sign
    private void ReadSign ()
    {
        DialogueController.instance.ChangeConversation("Signs/" + path);
    }
}
