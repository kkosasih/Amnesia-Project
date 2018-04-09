using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNightPanelChecker : MonoBehaviour {
    #region Attributes
    private GameObject DayNightPanel;
    #endregion

    #region Event Functions
    void Awake ()
    {
        DayNightPanel = GameObject.Find("DayNightPanel");
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (SceneManager.GetActiveScene ().name.Contains ("Interior"))
			DayNightPanel.SetActive (false);
		else
			DayNightPanel.SetActive (true);
	}
    #endregion
}
