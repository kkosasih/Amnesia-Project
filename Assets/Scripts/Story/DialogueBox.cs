using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    public bool isOpen = false;
    public Conversation conversation;
    private int convoIndex = 0;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        Image i = GetComponent<Image>();
        if (isOpen && !i.IsActive())
        {
            Helper.ChangeAllVisibility(gameObject, true);
        }
        else if (!isOpen && i.IsActive())
        {
            Helper.ChangeAllVisibility(gameObject, false);
        }
    }
    
    // Change the conversation
    public void ChangeConversation (Conversation newConvo)
    {
        transform.Find("NextButton").Find("Text").gameObject.GetComponent<Text>().text = "Next";
        conversation = newConvo;
        convoIndex = 0;
        ChangeStatement(conversation[0]);
    }

    // Move the conversation along
    public void AdvanceConversation()
    {
        if (convoIndex >= conversation.Count - 1)
        {
            transform.Find("NextButton").Find("Text").gameObject.GetComponent<Text>().text = "Close";
        }
        else
        {
            transform.Find("NextButton").Find("Text").gameObject.GetComponent<Text>().text = "Next";
        }
        if (++convoIndex >= conversation.Count)
        {
            isOpen = false;
        }
        else
        {
            ChangeStatement(conversation[convoIndex]);
        }
    }
    
    // Apply a new statement to the dialogue box
    public void ChangeStatement (Statement newStatement)
    {
        transform.Find("Portrait").gameObject.GetComponent<Image>().sprite = newStatement.portrait;
        transform.Find("Speaker").gameObject.GetComponent<Text>().text = newStatement.speaker;
        transform.Find("Speech").gameObject.GetComponent<Text>().text = newStatement.speech;
    }
}
