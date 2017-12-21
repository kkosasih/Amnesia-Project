using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    public List<DialoguePart> conversation;
    private int convoIndex = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!conversation[convoIndex].isRunning && convoIndex < conversation.Count)
        {
            AdvanceConversation();
        }
    }

    // Change the conversation
    public void ChangeConversation (List<DialoguePart> newConvo)
    {
        conversation = newConvo;
        convoIndex = -1;
        GameObject.FindWithTag("MainCamera").GetComponent<UITracking>().enabled = false;
        ++GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().movementPreventions;
        AdvanceConversation();
    }

    // Move the conversation along
    public void AdvanceConversation()
    {
        if (++convoIndex >= conversation.Count)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<UITracking>().enabled = true;
            --GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().movementPreventions;
        }
        else
        {
            ChangeStatement(conversation[convoIndex]);
            if (convoIndex >= conversation.Count - 1)
            {
                transform.Find("NextButton").Find("Text").gameObject.GetComponent<Text>().text = "Close";
            }
            else
            {
                transform.Find("NextButton").Find("Text").gameObject.GetComponent<Text>().text = "Next";
            }
        }
    }

    // Apply a new statement to the dialogue box
    public void ChangeStatement (DialoguePart newPart)
    {
        StartCoroutine(newPart.PerformPart());
    }

    // Test dialogue
    public void Test()
    {
        ChangeConversation(MakeDialogue(Resources.Load<TextAsset>("Conversations/Test.txt").text));
    }

    // Make a list of dialogue options from text
    public static List<DialoguePart> MakeDialogue (string data)
    {
        List<DialoguePart> result = new List<DialoguePart>();
        foreach (string s in data.Split(new string[] { "||" }, System.StringSplitOptions.None))
        {
            switch (s.Split(':')[0])
            {
                case "Statement":
                    result.Add(new Statement(s.Split(':')[1]));
                    break;
                case "Camera":
                    result.Add(new MoveCam(s.Split(':')[1]));
                    break;
                case "Character":
                    result.Add(new MoveChar(s.Split(':')[1]));
                    break;
            }
        }
        return result;
    }
}
