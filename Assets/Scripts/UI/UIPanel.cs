using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour {
    public bool isOpen = false;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isOpen && !GetComponent<Image>().IsActive())
        {
            Helper.ChangeAllVisibility(gameObject, true);
        }
        else if (!isOpen && GetComponent<Image>().IsActive())
        {
            Helper.ChangeAllVisibility(gameObject, false);
        }
    }

    // Close the panel
    public void Close ()
    {
        isOpen = false;
    }
}
