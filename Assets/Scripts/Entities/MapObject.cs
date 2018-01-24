using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour {
    public int tile;                // The tile that the object is placed on
    public Vector3 spriteOffset;    // The offest of the sprite compared to the tile

	// Use this for initialization
	void Start ()
    {
        transform.Find("Sprite").localPosition = spriteOffset;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Place the object
        if (GameController.map != null)
        {
            transform.position = new Vector3(GameController.map.tiles[tile].transform.position.x, GameController.map.tiles[tile].transform.position.y, BehindPlayer() ? -2 : -0.5f);
        }
    }

    // Returns true if the player is behind the object
    private bool BehindPlayer ()
    {
        return tile > GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().currentTile;
    }
}
