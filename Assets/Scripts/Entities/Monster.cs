using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {
    public PlayerCharacter player;  // The player script to track
    //public bool pickuptile = false;
    public GameObject canvas;

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
        // Find the player

        canvas = GameObject.Find("Canvas");
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        }
        // If not in a cutscene
        if (movementPreventions == 0)
        {
            if (Mathf.Max(Mathf.Abs(HoriDistance(player.currentTile)), Mathf.Abs(player.currentTile)) <= 2)
            {
                StartCoroutine(Attack(Direction.Up));
            }
            else if (lastMove >= delay && !moving)
            {
                StartCoroutine(AutoMoveOneStep(player.currentTile));
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
        canvas.GetComponent<QuestTracking>().questobj(name);
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

    public Vector3 Mlocation ()
    {
        return GameController.map.tiles[currentTile].transform.position;
    }
}