using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Apply a new statement to the dialogue box
    public void ChangeStatement (Statement newStatement)
    {
        transform.Find("Portrait").gameObject.GetComponent<Image>().sprite = newStatement.portrait;
        transform.Find("Speaker").gameObject.GetComponent<Text>().text = newStatement.speaker;
        transform.Find("Speech").gameObject.GetComponent<Text>().text = newStatement.speech;
    }
}
