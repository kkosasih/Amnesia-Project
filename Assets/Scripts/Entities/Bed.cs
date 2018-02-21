using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator sleep ()
	{
		GameObject player = GameObject.FindWithTag ("Player");
		player.GetComponent<PlayerCharacter> ().movementPreventions = 1;
		Image mask = GameObject.FindWithTag ("UIMask").GetComponent<Image> ();

		player.GetComponent<PlayerCharacter> ().ChangeStamina (player.GetComponent<PlayerCharacter> ().maxStamina);
		player.GetComponent<PlayerCharacter> ().ChangeHealth (player.GetComponent<PlayerCharacter> ().maxHealth);

		yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 1), 0.5f));
		yield return StartCoroutine (Helper.ChangeColorInTime (mask, new Color (0, 0, 0, 0), 0.5f));

		player.GetComponent<PlayerCharacter> ().movementPreventions = 0;

	}
}
