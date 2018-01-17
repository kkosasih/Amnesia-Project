using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    public static DialogueController instance;
    public char branch = 'A';
    public List<DialoguePart> conversation;
    private UIPanel dialoguePanel;
    private int convoIndex = 0;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        conversation = new List<DialoguePart>();
        dialoguePanel = GameObject.Find("DialogueBox").GetComponent<UIPanel>();
        //ChangeConversation("Opening");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (convoIndex < conversation.Count && !conversation[convoIndex].isRunning)
        {
            AdvanceConversation();
        }
        if (dialoguePanel.isOpen && Input.GetKeyDown(KeyCode.E))
        {
            dialoguePanel.Close();
        }
    }

    // Change the conversation with conversation data from a file
    public void ChangeConversation (string path)
    {  
        foreach (DialoguePart p in conversation)
        {
            Destroy(p.gameObject);
        }
        List<DialoguePart> newConvo = new List<DialoguePart>();
        string data = Resources.Load<TextAsset>("Conversations/" + path).text;
        foreach (string str in data.Split(new string[] { "||" }, System.StringSplitOptions.None))
        {
            string s = str.Trim();
            switch (s.Split(':')[0])
            {
                case "Statement":
                    newConvo.Add(((GameObject)Instantiate(Resources.Load("Conversations/StatementObject"), transform)).GetComponent<Statement>());
                    break;
                case "Question":
                    newConvo.Add((Instantiate(Resources.Load<GameObject>("Conversations/QuestionObject"), transform)).GetComponent<Question>());
                    break;
                case "Camera":
                    newConvo.Add(((GameObject)Instantiate(Resources.Load("Conversations/MoveCamObject"), transform)).GetComponent<MoveCam>());
                    break;
                case "Character":
                    newConvo.Add(((GameObject)Instantiate(Resources.Load("Conversations/MoveCharObject"), transform)).GetComponent<MoveChar>());
                    break;
                case "Mask":
                    newConvo.Add(((GameObject)Instantiate(Resources.Load("Conversations/ChangeMaskObject"), transform)).GetComponent<ChangeMask>());
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
        ChangeConversation("Test");
    }
}
