using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MapObject : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;     // The Sprite Renderer component attached to child
    private static SpriteRenderer playerSprite; // The player's sprite to use for effect

	// Use this for initialization
	void Start ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (playerSprite == null)
        {
            playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move in front of player if player is behind it
        _spriteRenderer.sortingOrder = playerSprite.sortingOrder - PlayerDist();
    }

    // Returns true if the player is behind the object
    private int PlayerDist ()
    {
        return (int)(transform.position.y - playerSprite.transform.position.y);
    }
}
