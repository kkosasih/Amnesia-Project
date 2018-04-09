using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entrance : StaticObject {
    #region Attributes
    public int sceneTo;                 // The scene index to load
    public int tileFrom;                // The tile to enter at
    public Direction moveTo;            // The direction to automatically move when entering
    private AudioSource entranceAudio;  // The audio played when an entrance is used
    #endregion

    #region Event Functions
    void Awake ()
    {
        entranceAudio = GetComponent<AudioSource>();
        GetComponent<Interactible>().interact = StartTeleport;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Methods
    // Function to start the teleportation
    public void StartTeleport ()
    {
        StartCoroutine(TeleportPlayer());
    }
    #endregion

    #region Coroutines
    // Teleport the player to the other side of the entrance
    public IEnumerator TeleportPlayer ()
    {
        // Set up object for its coroutine and start it
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        Image mask = GameObject.FindWithTag("UIMask").GetComponent<Image>();
        PlayerCharacter.instance.startTile = tileFrom;
        entranceAudio.time = 0.5f;
        entranceAudio.Play();
        yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 1), 0.5f));
        yield return StartCoroutine(GameController.instance.SetUpScene(sceneTo));
        // Check if there's no cutscene playing upon entering
        if (!DialogueTracking.CheckConversation())
        {
            yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 0), 0.5f));
            int tileTo = 0;
            // Set up the tile to move to based on direction
            switch (moveTo)
            {
                case Direction.Up:
                    tileTo = GameController.instance.map.TileAbove(PlayerCharacter.instance.CurrentTile);
                    break;
                case Direction.Down:
                    tileTo = GameController.instance.map.TileBelow(PlayerCharacter.instance.CurrentTile);
                    break;
                case Direction.Left:
                    tileTo = GameController.instance.map.TileLeft(PlayerCharacter.instance.CurrentTile);
                    break;
                case Direction.Right:
                    tileTo = GameController.instance.map.TileRight(PlayerCharacter.instance.CurrentTile);
                    break;
            }
            PlayerCharacter.instance.Move(tileTo, moveTo);
        }
        Destroy(gameObject);
    }
    #endregion
}
