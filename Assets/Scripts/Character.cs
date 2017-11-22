using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public static GameController controller;
    public int teamID;
    public int currentTile;
    public bool movementEnabled = true;
    public int health;
    public int maxHealth;
    public GameObject healthSlider;

    protected virtual void Awake ()
    {
        healthSlider = Instantiate((GameObject)Resources.Load("CharHealthSlider"), GameObject.Find("Canvas").transform);
        controller = GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
    }

    // Use this for initialization
    protected virtual void Start ()
    {
        Move(currentTile);
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
		if (health <= 0)
        {
            Die();
        }
	}

    // Move the character to another tile
    public virtual void Move (int moveTo)
    {
        if (controller.map.tiles[moveTo].GetComponent<Tile>().type == TileType.Ground)
        {
            currentTile = moveTo;
            transform.position = controller.map.tiles[currentTile].transform.position;
        }
    }

    // Put an attack on a tile for a given time
    public void Attack (int tile, int damage, float duration)
    {
        if (tile != -1)
        {
            StartCoroutine(controller.map.tiles[tile].GetComponent<Tile>().GiveAttack(teamID, damage, duration));
        }
    }

    // Kill this character
    public virtual void Die ()
    {
        Destroy(healthSlider.gameObject);
        Destroy(gameObject);
    }

    // Change the health of the character to the new value
    public void ChangeHealth (int newHealth)
    {
        if (health < newHealth)
        {
            StartCoroutine(LoseHealth(newHealth));
        }
        else
        {
            StartCoroutine(GainHealth(newHealth));
        }
        health = newHealth;
    }

    // Has the health bar react to gaining health
    private IEnumerator GainHealth (int newHealth)
    {
        healthSlider.transform.GetChild(0).Find("Fill Area").Find("Fill").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        healthSlider.transform.GetChild(0).gameObject.GetComponent<Slider>().value = newHealth * 100f / maxHealth;
        yield return new WaitForSeconds(1);
        healthSlider.transform.GetChild(1).gameObject.GetComponent<Slider>().value = newHealth * 100f / maxHealth;
    }

    // Has the health bar react to losing health
    private IEnumerator LoseHealth (int newHealth)
    {
        healthSlider.transform.GetChild(1).gameObject.GetComponent<Slider>().value = newHealth * 100f / maxHealth;
        healthSlider.transform.GetChild(0).Find("Fill Area").Find("Fill").gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1, 0);
        yield return new WaitForSeconds(1);
        healthSlider.transform.GetChild(0).gameObject.GetComponent<Slider>().value = newHealth * 100f / maxHealth;
    }
}
