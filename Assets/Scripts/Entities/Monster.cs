using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {
    public PlayerCharacter player;  // The player script to track
    //public bool pickuptile = false;

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
        // Find the player
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        }
        // If not in a cutscene
        if (movementPreventions == 0)
        {
            if (Mathf.Max(Mathf.Abs(HoriDistance()), Mathf.Abs(VertDistance())) <= 2)
            {
                StartCoroutine(Attack(Direction.Up));
            }
            else if (lastMove >= delay && !moving)
            {
                MoveToPlayer();
            }
        }
    }

    // Attack in a given direction dir
    public override IEnumerator Attack (Direction dir)
    {
        ++movementPreventions;
        yield return new WaitForSeconds(1);
        AttackController.instance.BurstAttack(new Attack(teamID, 1, 0.5f), currentTile, 2, 2);
        yield return new WaitForSeconds(0.5f);
        --movementPreventions;
    }

    // Kill this character and drop an item
    public override void Die ()
    {
        //inventory.GetComponent<QuestTracking>().questobj(name); //It's here.
        GameObject tile = GameController.map.tiles[currentTile];
        tile.GetComponent<Tile>().type = TileType.Pickup;
        // If the tile isn't already a pickup tile
        if (tile.GetComponent<PickupInventory>() == null)
        {
            tile.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Item Icons/vest");
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
            tile.AddComponent<PickupInventory>();
            tile.GetComponent<PickupInventory>().SetSize(4);
        }
        // Add items here
        tile.GetComponent<PickupInventory>().AddItemByID(2);
        base.Die();
    }

    // Move closer to the player and reset the timing of movement to 0
    private void MoveToPlayer ()
    {
        if (Mathf.Abs(VertDistance()) > Mathf.Abs(HoriDistance()) && VertDistance() > 0)
        {
            Move(GameController.map.TileAbove(currentTile), Direction.Up);
        }
        else if (Mathf.Abs(VertDistance()) > Mathf.Abs(HoriDistance()) && VertDistance() < 0)
        {
            Move(GameController.map.TileBelow(currentTile), Direction.Down);
        }
        else if (Mathf.Abs(HoriDistance()) >= Mathf.Abs(VertDistance()) && HoriDistance() > 0)
        {
            Move(GameController.map.TileLeft(currentTile), Direction.Left);
        }
        else if (Mathf.Abs(HoriDistance()) >= Mathf.Abs(VertDistance()) && HoriDistance() < 0)
        {
            Move(GameController.map.TileRight(currentTile), Direction.Right);
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