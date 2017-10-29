using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            shiftSave(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            shiftSave(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            shiftSave(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            shiftSave(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            shiftSave(4);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            shiftSave(5);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            shiftSave(6);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            shiftSave(7);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            shiftSave(8);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            shiftSave(9);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            player.transform.Translate(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            player.transform.Translate(0, -1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            player.transform.Translate(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            player.transform.Translate(1, 0, 0);
        }
            
    }

    private void shiftSave (int slot)
    {
        if (Input.GetKey(KeyCode.S))
        {
            SaveLoad.Save(slot);
        }
        else
        {
            SaveLoad.Load(slot);
        }
    }
}
