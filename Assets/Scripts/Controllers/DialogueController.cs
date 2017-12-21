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

    // Change the conversation
    public void ChangeConversation (List<DialoguePart> newConvo)
    {
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
        ChangeConversation(MakeDialogue(Resources.Load<TextAsset>("Conversations/Test").text));
    }

    // Make a list of dialogue options from text
    public List<DialoguePart> MakeDialogue (string data)
    {
        foreach (DialoguePart p in conversation)
        {
            Destroy(p.gameObject);
        }
        List<DialoguePart> result = new List<DialoguePart>();
        foreach (string s in data.Split(new string[] { "||" }, System.StringSplitOptions.None))
        {
            switch (s.Split(':')[0])
            {
                case "Statement":
                    result.Add(((GameObject)Instantiate(Resources.Load("Conversations/StatementObject"), transform)).GetComponent<Statement>());
                    break;
                case "Camera":
                    result.Add(((GameObject)Instantiate(Resources.Load("Conversations/MoveCamObject"), transform)).GetComponent<MoveCam>());
                    break;
                case "Character":
                    result.Add(((GameObject)Instantiate(Resources.Load("Conversations/MoveCharObject"), transform)).GetComponent<MoveChar>());
                    break;
            }
            result[result.Count - 1].ChangeSettings(s.Split(':')[1]);
        }
        return result;
    }
}
