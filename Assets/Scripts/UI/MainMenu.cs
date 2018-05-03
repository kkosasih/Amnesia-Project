using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu {
    #region Attributes
    [SerializeField]
    private List<GameObject> hidden;    // The game objects to disable until game starts
    #endregion

    #region Properties

    #endregion

    #region Event Functions
    // Use this for initialization
    protected override void Start()
    {
        foreach (GameObject g in hidden)
        {
            g.SetActive(false);
        }
    }
    #endregion

    #region Methods
    // Start the game
    public void StartGame ()
    {
        foreach (GameObject g in hidden)
        {
            g.SetActive(true);
        }
        GameController.instance.SetUpSceneFunc("Beach1");
    }

    // Quit the game
    public void QuitGame ()
    {
        Application.Quit();
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
