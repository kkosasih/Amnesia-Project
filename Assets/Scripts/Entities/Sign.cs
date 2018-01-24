using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour {
    public string path; // The name of the text file to reference

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Have the player read the sign
    public void ReadSign ()
    {
        DialogueController.instance.ChangeConversation("Signs/" + path);
    }
}
