using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : StaticObject {
    #region Attributes
    public string path; // The name of the text file to reference
    #endregion

    #region Event Functions
    void Awake ()
    {
        GetComponent<Interactible>().interact = ReadSign;
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion

    #region Methods
    // Read the sign
    private void ReadSign ()
    {
        DialogueController.instance.ChangeConversation("Signs/" + path);
    }
    #endregion
}
