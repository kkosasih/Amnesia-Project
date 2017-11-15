using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {
    public PlayerCharacter player;
    public float delay;
    private float lastMove = 0;
    
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

    // Move and be attacked if needed
    public override void Move (int moveTo)
    {
        base.Move(moveTo);
        GameObject tile = controller.map.tiles[currentTile];
        if (tile.GetComponent<Tile>().attackID == 1)
        {
            ChangeHealth(health - tile.GetComponent<Tile>().attackDamage);
        }
    }

    // Kill this character and drop an item
    public override void Die()
    {
        GetComponent<EnemyItemDropScript>().EnemyDied();
        Destroy(gameObject);
    }

    // Move closer to the player and reset the timing of movement to 0
    private void MoveToPlayer ()
    {
        lastMove = 0;
        int horiDistance = currentTile % controller.map.width - player.currentTile % controller.map.width;
        int vertDistance = currentTile / controller.map.width - player.currentTile / controller.map.width;
        if (Mathf.Abs(vertDistance) > Mathf.Abs(horiDistance) && vertDistance < 0)
        {
            Move(controller.map.TileAbove(currentTile));
        }
        else if (Mathf.Abs(vertDistance) > Mathf.Abs(horiDistance) && vertDistance > 0)
        {
            Move(controller.map.TileBelow(currentTile));
        }
        else if (Mathf.Abs(horiDistance) >= Mathf.Abs(vertDistance) && horiDistance < 0)
        {
            Move(controller.map.TileLeft(currentTile));
        }
        else if (Mathf.Abs(horiDistance) >= Mathf.Abs(vertDistance) && horiDistance > 0)
        {
            Move(controller.map.TileRight(currentTile));
        }
    }
}