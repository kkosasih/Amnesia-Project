using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    #region Attributes
    public static DialogueController instance;  // The instance to reference in other scripts
    private int movementPreventions = 0;         // The movement preventions on the game (in cutscene)
    private char branch = '*';                   // The current branch of dialogue the player has
    private List<DialoguePart> conversation;     // The current cutscene to play
    private string tree;                        // The name of the current set of branches
    private UIPanel dialoguePanel;              // The dialogue panel to reference
    private int convoIndex = 0;                 // The current part of the cutscene playing
    #endregion

    #region Properties
    // Returns and clamps movementPreventions to be min 0
    public int MovementPreventions
    {
        get
        {
            return movementPreventions;
        }
        set
        {
            movementPreventions = Mathf.Max(0, value);
        }
    }

    // Returns and sets branch
    public char Branch
    {
        get
        {
            return branch;
        }
        set
        {
            branch = value;
        }
    }

    // Returns conversation
    public List<DialoguePart> Conversation
    {
        get
        {
            return conversation;
        }
    }
    #endregion

    #region Event Functions
    private void Awake ()
    {
        instance = this;
        conversation = new List<DialoguePart>();
        dialoguePanel = GameObject.Find("DialogueBox").GetComponent<UIPanel>();
        //ChangeConversation("Opening");
    }

    // Use this for initialization
    void Start ()
    {
        Test();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Advance if the current part is ended
        if (convoIndex < conversation.Count && !conversation[convoIndex].isRunning)
        {
            AdvanceConversation();
        }
        if (dialoguePanel.IsOpen && Input.GetKeyDown(KeyCode.E))
        {
            dialoguePanel.Close();
        }
    }
    #endregion

    #region Methods
    // Change the conversation with conversation data from a file
    public void ChangeConversation (string path)
    {
        ++movementPreventions;
        // Remove old parts
        foreach (DialoguePart p in conversation)
        {
            Destroy(p.gameObject);
        }
        // Add new parts based on given text file
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
        // Stop all characters' movement
        
        tree = path;
        branch = '*';
        AdvanceConversation();
    }

    // Move the conversation along
    public void AdvanceConversation ()
    {
        // Change the branch or end the cutscene if it's over
        if (++convoIndex >= conversation.Count)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CameraTracking>().enabled = true;
            --movementPreventions;
            if (branch != '*')
            {
                ChangeConversation(tree + branch);
            }
        }
        else
        {
            ChangeStatement(conversation[convoIndex]);
            // Change the button to "close" if it's the last part of the cutscene
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
    #endregion
}
