using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameObject player;
    public static GameObject inventory;
    public static Map map;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        inventory = GameObject.Find("Inventory");
        StartCoroutine(SetUpScene(SceneManager.GetActiveScene().buildIndex));

        Inventory inv = inventory.GetComponent<Inventory>();
        inv.AddItemByID(0);
        for (int i = 0; i < 5; ++i)
        {
            inv.AddItemByID(2);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ShiftSave(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShiftSave(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShiftSave(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShiftSave(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShiftSave(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ShiftSave(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ShiftSave(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ShiftSave(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ShiftSave(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ShiftSave(9);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.GetComponent<UIPanel>().isOpen = !inventory.GetComponent<UIPanel>().isOpen;
        }
    }

    // Attach a map to the player to use
    public static void FindMap ()
    { 
        map = GameObject.FindWithTag("Map").GetComponent<Map>();
    }

    // Load a scene and set up necessary parts
    public static IEnumerator SetUpScene (int index)
    {
        if (SceneManager.GetActiveScene().buildIndex != index)
        {
            AsyncOperation a = SceneManager.LoadSceneAsync(index);
            while (!a.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        FindMap();
        player.GetComponent<PlayerCharacter>().PlaceOnMap();
        foreach (Character c in GameObject.Find("Characters").transform.GetComponentsInChildren<Character>())
        {
            c.PlaceOnMap();
        }
    }

    // Save and load based on shift key
    private static void ShiftSave (int slot)
    {
        if (Input.GetKey(KeyCode.Z))
        {
            SaveLoad.Save(slot);
        }
        else
        {
            SaveLoad.Load(slot);
        }
    }
}
