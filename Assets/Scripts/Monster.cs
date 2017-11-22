using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {
    public PlayerCharacter player;
    public float delay;
    private float lastMove = 0;

    // Use this for initialization
    protected override void Start ()
    {   
        base.Start();
        healthSlider.GetComponent<UITracking>().obj = gameObject;
        healthSlider.GetComponent<UITracking>().cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
		if (lastMove >= delay)
        {
            MoveToPlayer();
        }
        else
        {
            lastMove += Time.deltaTime;
        }
	}

    // Kill this character and drop an item
    public override void Die ()
    {
        base.Die();
        GetComponent<EnemyItemDropScript>().EnemyDied();
    }

    // Move closer to the player and reset the timing of movement to 0
    private void MoveToPlayer ()
    {
        lastMove = 0;
        int horiDistance = currentTile % controller.map.width - player.currentTile % controller.map.width;
        int vertDistance = currentTile / controller.map.width - player.currentTile / controller.map.width;
        if (Mathf.Abs(vertDistance) > Mathf.Abs(horiDistance) && vertDistance > 0)
        {
            Move(controller.map.TileAbove(currentTile));
        }
        else if (Mathf.Abs(vertDistance) > Mathf.Abs(horiDistance) && vertDistance < 0)
        {
            Move(controller.map.TileBelow(currentTile));
        }
        else if (Mathf.Abs(horiDistance) >= Mathf.Abs(vertDistance) && horiDistance > 0)
        {
            Move(controller.map.TileLeft(currentTile));
        }
        else if (Mathf.Abs(horiDistance) >= Mathf.Abs(vertDistance) && horiDistance < 0)
        {
            Move(controller.map.TileRight(currentTile));
        }
    }
}