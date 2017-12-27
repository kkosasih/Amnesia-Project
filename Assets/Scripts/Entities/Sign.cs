using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour {
    public string path;

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
        DialogueController.instance.ChangeConversation(Resources.Load<TextAsset>("Conversations/Signs/" + path).text);
    }
}
