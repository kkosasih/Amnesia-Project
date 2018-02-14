using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entrance : MonoBehaviour {
    public int sceneTo;         // The scene index to load
    public int tileFrom;        // The tile to enter at
    public Direction moveTo;    // The direction to automatically move when entering
    private AudioSource entranceAudio; // The audio played when an entrance is used

    private void Awake()
    {
        entranceAudio = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    // Teleport the player to the other side of the entrance
    public IEnumerator TeleportPlayer ()
    {
        // Set up object for its coroutine and start it
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        GameObject player = GameObject.FindWithTag("Player");
        Image mask = GameObject.FindWithTag("UIMask").GetComponent<Image>();
        player.GetComponent<PlayerCharacter>().onMap = false;
        entranceAudio.time = 0.5f;
        entranceAudio.Play();
        yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 1), 0.5f));
        player.GetComponent<PlayerCharacter>().currentTile = tileFrom;
        yield return StartCoroutine(GameController.SetUpScene(sceneTo));
        // Check if there's no cutscene playing upon entering
        if (!DialogueTracking.CheckConversation())
        {
            yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 0), 0.5f));
            int tileTo = 0;
            // Set up the tile to move to based on direction
            switch (moveTo)
            {
                case Direction.Up:
                    tileTo = GameController.map.TileAbove(player.GetComponent<PlayerCharacter>().currentTile);
                    break;
                case Direction.Down:
                    tileTo = GameController.map.TileBelow(player.GetComponent<PlayerCharacter>().currentTile);
                    break;
                case Direction.Left:
                    tileTo = GameController.map.TileLeft(player.GetComponent<PlayerCharacter>().currentTile);
                    break;
                case Direction.Right:
                    tileTo = GameController.map.TileRight(player.GetComponent<PlayerCharacter>().currentTile);
                    break;
            }
            player.GetComponent<PlayerCharacter>().Move(tileTo, moveTo);
        }
        Destroy(gameObject);
    }
}
