using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {
    public PlayerCharacter player;
    public bool pickuptile = false;

    protected override void Awake()
    {
        base.Awake();
        UITracking uit = healthSlider.GetComponent<UITracking>();
        uit.obj = gameObject;
        uit.offset = new Vector3(0, 40, 0);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (HoriDistance() + VertDistance() <= 1)
        {
            if (HoriDistance() > 0)
            {
                Attack(GameController.map.TileLeftStrict(currentTile), 1, 1.0f);
            }
            else if (HoriDistance() < 0)
            {
                Attack(GameController.map.TileRightStrict(currentTile), 1, 1.0f);
            }
            else if (VertDistance() > 0)
            {
                Attack(GameController.map.TileAboveStrict(currentTile), 1, 1.0f);
            }
            else if (VertDistance() < 0)
            {
                Attack(GameController.map.TileBelowStrict(currentTile), 1, 1.0f);
            }
        }
        if (lastMove >= delay)
        {
            MoveToPlayer();
        }
    }

    // Kill this character and drop an item
    public override void Die ()
    {
        base.Die();
        if(GameController.map.tiles[currentTile].GetComponent<Tile>().type != TileType.Pickup)
        {
            pickuptile = true;
        }
        GetComponent<EnemyItemDropScript>().EnemyDied(pickuptile);
    }

    // Move closer to the player and reset the timing of movement to 0
    private void MoveToPlayer ()
    {
        if (Mathf.Abs(VertDistance()) > Mathf.Abs(HoriDistance()) && VertDistance() > 0)
        {
            Move(GameController.map.TileAbove(currentTile));
        }
        else if (Mathf.Abs(VertDistance()) > Mathf.Abs(HoriDistance()) && VertDistance() < 0)
        {
            Move(GameController.map.TileBelow(currentTile));
        }
        else if (Mathf.Abs(HoriDistance()) >= Mathf.Abs(VertDistance()) && HoriDistance() > 0)
        {
            Move(GameController.map.TileLeft(currentTile));
        }
        else if (Mathf.Abs(HoriDistance()) >= Mathf.Abs(VertDistance()) && HoriDistance() < 0)
        {
            Move(GameController.map.TileRight(currentTile));
        }
    }

    // Get the horizontal distance from player; negative is player to right, positive is player to left
    private int HoriDistance ()
    {
        return currentTile % GameController.map.width - player.currentTile % GameController.map.width;
    }

    // Get the vertical distance from player; negative is player below, positive is player above
    private int VertDistance ()
    {
        return currentTile / GameController.map.width - player.currentTile / GameController.map.width;
    }

    public Vector3 Mlocation ()
    {
        return GameController.map.tiles[currentTile].transform.position;
    }
}