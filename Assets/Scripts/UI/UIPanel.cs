using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour {
    #region Attributes
    private bool isOpen = false; // Whether the UI element should be visible
    #endregion

    #region Properties
    // Returns and sets isOpen
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
        set
        {
            isOpen = value;
        }
    }
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {  
        // Change visibility based on isOpen
        if (isOpen && !GetComponent<Image>().IsActive())
        {
            Helper.ChangeAllVisibility(gameObject, true);
        }
        else if (!isOpen && GetComponent<Image>().IsActive())
        {
            Helper.ChangeAllVisibility(gameObject, false);
        }
    }
    #endregion

    #region Methods
    // Close the panel (for buttons)
    public void Close ()
    {
        isOpen = false;
    }
    #endregion
}
