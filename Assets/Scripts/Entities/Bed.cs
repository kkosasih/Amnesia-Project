using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour {

    #region Event Functions
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion

    #region Coroutines
    public IEnumerator sleep ()
	{
        ++DialogueController.instance.MovementPreventions;
		Image mask = GameObject.FindWithTag ("UIMask").GetComponent<Image> ();

        PlayerCharacter.instance.ChangeStamina (PlayerCharacter.instance.MaxStamina);
        PlayerCharacter.instance.SetHealth(PlayerCharacter.instance.MaxHealth);

		yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 1), 0.5f));
		yield return StartCoroutine (Helper.ChangeColorInTime (mask, new Color (0, 0, 0, 0), 0.5f));

        --DialogueController.instance.MovementPreventions;

    }
    #endregion
}
