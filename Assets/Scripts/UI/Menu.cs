 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    #region Attributes
    [SerializeField]
    protected List<GameObject> pages;   // The pages to navigate to
	#endregion
	
	#region Properties
	
	#endregion

	#region Event Functions
	// Awake is called before Start
	protected virtual void Awake ()
	{
        ChangePage(0);
	}

    // Use this for initialization
    protected virtual void Start () 
	{
		
	}

    // Update is called once per frame
    protected void Update () 
	{
		
	}
	#endregion
	
	#region Methods
	// Navigate to another page
    public void ChangePage (int page)
    {
        for (int i = 0; i < pages.Count; ++i)
        {
            pages[i].SetActive(i == page);
        }
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
