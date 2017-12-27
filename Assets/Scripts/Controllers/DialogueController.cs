﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    public static DialogueController instance;
    public List<DialoguePart> conversation;
    private int convoIndex = 0;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        conversation = new List<DialoguePart>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (convoIndex < conversation.Count && !conversation[convoIndex].isRunning)
        {
            AdvanceConversation();
        }
    }

    // Change the conversation with conversation data from a file
    public void ChangeConversation (string data)
    {
        foreach (DialoguePart p in conversation)
        {
            Destroy(p.gameObject);
        }
        List<DialoguePart> newConvo = new List<DialoguePart>();
        foreach (string str in data.Split(new string[] { "||" }, System.StringSplitOptions.None))
        {
            string s = str.Trim();
            switch (s.Split(':')[0])
            {
                case "Statement":
                    newConvo.Add(((GameObject)Instantiate(Resources.Load("Conversations/StatementObject"), transform)).GetComponent<Statement>());
                    break;
                case "Camera":
                    newConvo.Add(((GameObject)Instantiate(Resources.Load("Conversations/MoveCamObject"), transform)).GetComponent<MoveCam>());
                    break;
                case "Character":
                    newConvo.Add(((GameObject)Instantiate(Resources.Load("Conversations/MoveCharObject"), transform)).GetComponent<MoveChar>());
                    break;
            }
            newConvo[newConvo.Count - 1].ChangeSettings(s.Split(':')[1]);
        }
        conversation = newConvo;
        convoIndex = -1;
        GameObject.FindWithTag("MainCamera").GetComponent<CameraTracking>().enabled = false;
        ++GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().movementPreventions;
        AdvanceConversation();
    }

    // Move the conversation along
    public void AdvanceConversation ()
    {
        if (++convoIndex >= conversation.Count)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CameraTracking>().enabled = true;
            --GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().movementPreventions;
        }
        else
        {
            ChangeStatement(conversation[convoIndex]);
            if (convoIndex >= conversation.Count - 1)
            {
                GameObject.Find("DialogueBox").transform.Find("NextButton").Find("Text").gameObject.GetComponent<Text>().text = "Close";
            }
            else
            {
                GameObject.Find("DialogueBox").transform.Find("NextButton").Find("Text").gameObject.GetComponent<Text>().text = "Next";
            }
        }
    }

    // Apply a new statement to the dialogue box
    public void ChangeStatement (DialoguePart newPart)
    {
        StartCoroutine(newPart.PerformPart());
    }

    // Test dialogue
    public void Test ()
    {
        ChangeConversation(Resources.Load<TextAsset>("Conversations/Test").text);
    }
}
