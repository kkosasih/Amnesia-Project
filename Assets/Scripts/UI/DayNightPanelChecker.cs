using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNightPanelChecker : MonoBehaviour {

	private GameObject DayNightPanel;
	// Use this for initialization
	void Start () {
		DayNightPanel = GameObject.Find ("DayNightPanel");
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name.Contains ("Interior"))
			DayNightPanel.SetActive (false);
		else
			DayNightPanel.SetActive (true);
	}
}
