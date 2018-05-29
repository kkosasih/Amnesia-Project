using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entrance : StaticObject {
    #region Attributes
    public string sceneTo;                      // The scene to load
    public int tileFrom;                        // The tile to enter at
    public Direction moveTo;                    // The direction to automatically move when entering
    [SerializeField]
    protected List<AudioClip> entranceAudio;    // The audio played when an entrance is used
    protected AudioSource _audioSource;         // The Audio Source component attached
    #endregion

    #region Event Functions
    void Awake ()
    {
        _audioSource = GetComponent<AudioSource>();
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
		PlayerCharacter.instance.MovementPreventions++;
        _audioSource.clip = entranceAudio[Random.Range(0, entranceAudio.Count)];
        _audioSource.Play();
        yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 1), 0.5f));
        yield return StartCoroutine(GameController.instance.SetUpScene(sceneTo));
        yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 0), 0.5f));
        int tileTo = GameController.instance.map.TileInDirection(moveTo, PlayerCharacter.instance.CurrentTile);
        PlayerCharacter.instance.Move(tileTo, moveTo);
		PlayerCharacter.instance.MovementPreventions--;
        Destroy(gameObject);
    }
    #endregion
}
