using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    #region Attributes
    public static GameObject player;    // The player object to track
    public static GameObject inventory; // The player inventory to track
    public static Map map;              // The game map to track
    #endregion

    #region Event Functions
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
            inventory.GetComponent<UIPanel>().IsOpen = !inventory.GetComponent<UIPanel>().IsOpen;
        }
    }
    #endregion

    #region Methods
    // Attach a map to the player to use
    public static void FindMap ()
    { 
        map = GameObject.FindWithTag("Map").GetComponent<Map>();
    }

    // Save and load based on shift key
    private static void ShiftSave(int slot)
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
    #endregion

    #region Coroutines
    // Load a scene and set up necessary parts
    public static IEnumerator SetUpScene (int index)
    {
        if (SceneManager.GetActiveScene().buildIndex != index)
        {
            // Wait for scene to fully load
            AsyncOperation a = SceneManager.LoadSceneAsync(index);
            while (!a.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        FindMap();
        PlayerCharacter.instance.PlaceOnMap(PlayerCharacter.instance.CurrentTile);
        // Place static characters on map
        foreach (StaticObject s in GameObject.Find("Characters").transform.GetComponentsInChildren<StaticObject>())
        {
            s.PlaceOnMap(s.startTile);
        }
        foreach (StaticObject s in GameObject.Find("MapObjects").transform.GetComponentsInChildren<StaticObject>())
        {
            s.PlaceOnMap(s.startTile);
        }
    }
    #endregion
}
