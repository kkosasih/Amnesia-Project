using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonEntrance : Entrance 
{
    #region Variables
    public int roomTo;  // The room to travel to
	#endregion
	
	#region Properties
	
	#endregion
	
	#region Events
    /// <summary>
    /// Awake is called before Start
    /// </summary>
	private void Awake() 
	{
		
	}

    /// <summary>
    ///  Use this for initialization
    /// </summary>
    private void Start() 
	{
		
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update() 
	{
		
	}
    #endregion

    #region Methods

    #endregion

    #region Coroutines
    // Teleport the player to the other side of the entrance
    public override IEnumerator TeleportPlayer()
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
        Dungeon.instance.roomIndex = roomTo;
        PlayerCharacter.instance.Move(tileTo, moveTo);
        PlayerCharacter.instance.MovementPreventions--;
        Destroy(gameObject);
    }
    #endregion
}
