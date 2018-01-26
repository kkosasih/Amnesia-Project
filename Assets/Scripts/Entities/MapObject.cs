using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour {
    public int tile;                        // The tile that the object is placed on
    public Vector3 spriteOffset;            // The offest of the sprite compared to the tile
    private SpriteRenderer _spriteRenderer; // The Sprite Renderer component attached to child

	// Use this for initialization
	void Start ()
    {
        transform.Find("Sprite").localPosition = spriteOffset;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Place the object
        if (GameController.map != null)
        {
            transform.position = GameController.map.tiles[tile].transform.position;
        }
        // Move in front of player if player is behind it
        _spriteRenderer.sortingOrder = BehindPlayer() ? 200 : 50;
    }

    // Returns true if the player is behind the object
    private bool BehindPlayer ()
    {
        return tile > GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>().currentTile;
    }
}
