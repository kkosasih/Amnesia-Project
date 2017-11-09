using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public static GameController controller;
    public int currentTile;
    public bool movementEnabled = true;
    public int health;
    public int maxHealth;
    protected Slider healthSlider;

    protected virtual void Awake ()
    {
        controller = GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
    }

    // Use this for initialization
    protected void Start ()
    {
        Move(currentTile);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Move the character to another tile
    public virtual void Move (int moveTo)
    {
        if (controller.map.tiles[moveTo].GetComponent<Tile>().type == Tile.TileType.Ground)
        {
            currentTile = moveTo;
            transform.position = controller.map.tiles[currentTile].transform.position;
        }
    }

    // Put an attack on a tile for a given time
    public void Attack (int tile, float duration)
    {
        if (tile != -1)
        {
            StartCoroutine(controller.map.tiles[tile].GetComponent<Tile>().GiveAttack(duration));
        }
    }

    // Change the health of the character to the new value
    public void ChangeHealth (int newHealth)
    {
        health = newHealth;
        healthSlider.value = (float)health / maxHealth * 100;
    }
}
