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
                Attack(controller.map.TileLeftStrict(currentTile), 1, 1.0f);
            }
            else if (HoriDistance() < 0)
            {
                Attack(controller.map.TileRightStrict(currentTile), 1, 1.0f);
            }
            else if (VertDistance() > 0)
            {
                Attack(controller.map.TileAboveStrict(currentTile), 1, 1.0f);
            }
            else if (VertDistance() < 0)
            {
                Attack(controller.map.TileBelowStrict(currentTile), 1, 1.0f);
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
        if(controller.map.tiles[currentTile].GetComponent<Tile>().type != TileType.Pickup)
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
            Move(controller.map.TileAbove(currentTile));
        }
        else if (Mathf.Abs(VertDistance()) > Mathf.Abs(HoriDistance()) && VertDistance() < 0)
        {
            Move(controller.map.TileBelow(currentTile));
        }
        else if (Mathf.Abs(HoriDistance()) >= Mathf.Abs(VertDistance()) && HoriDistance() > 0)
        {
            Move(controller.map.TileLeft(currentTile));
        }
        else if (Mathf.Abs(HoriDistance()) >= Mathf.Abs(VertDistance()) && HoriDistance() < 0)
        {
            Move(controller.map.TileRight(currentTile));
        }
    }

    // Get the horizontal distance from player; negative is player to right, positive is player to left
    private int HoriDistance ()
    {
        return currentTile % controller.map.width - player.currentTile % controller.map.width;
    }

    // Get the vertical distance from player; negative is player below, positive is player above
    private int VertDistance ()
    {
        return currentTile / controller.map.width - player.currentTile / controller.map.width;
    }

    public Vector3 Mlocation ()
    {
        return controller.map.tiles[currentTile].transform.position;
    }
}