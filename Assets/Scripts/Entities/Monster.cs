using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {
    //public PlayerCharacter player;  // The player script to track
    //public bool pickuptile = false;
    public GameObject canvas;

    // Update is called once per frame
    protected override void Update ()
    {
        canvas = GameObject.Find("Canvas");
        // If not in a cutscene
        if (DialogueController.instance.movementPreventions + movementPreventions == 0 && onMap)
        {
            if (Mathf.Max(Mathf.Abs(HoriDistance(GameController.map.takenTiles[PlayerCharacter.instance])), Mathf.Abs(GameController.map.takenTiles[PlayerCharacter.instance])) <= 2)
            {
                StartCoroutine(Attack(Direction.Up));
            }
            else if (lastMove >= delay && !moving)
            {
                StartCoroutine(AutoMoveOneStep(GameController.map.takenTiles[PlayerCharacter.instance]));
            }
        }
        base.Update();
    }

    // Attack in a given direction dir
    public override IEnumerator Attack (Direction dir)
    {
        ++movementPreventions;
        yield return new WaitForSeconds(1);
        AttackController.instance.BurstAttack(new Attack(teamID, 1, 0.5f), GameController.map.takenTiles[this], 2, 2);
        yield return new WaitForSeconds(0.5f);
        --movementPreventions;
    }

    // Kill this character and drop an item
    public override void Die ()
    {
        canvas.GetComponent<QuestTracking>().questobj(name);
        GameObject drop = DropAtTile();
        // If the tile isn't already a pickup tile
        if (drop == null)
        {
            drop = Instantiate(Resources.Load<GameObject>("ItemDrop"));
            StaticObject s = drop.GetComponent<StaticObject>();
            s.startTile = GameController.map.takenTiles[this];
            s.PlaceOnMap();
            drop.GetComponent<PickupInventory>().SetSize(4);
        }
        // Add items here
        drop.GetComponent<PickupInventory>().AddItemByID(2);
        base.Die();
    }

    public Vector3 Mlocation ()
    {
        return GameController.map.tiles[GameController.map.takenTiles[this]].transform.position;
    }

    // Returns a drop at the tile if present
    private GameObject DropAtTile ()
    {
        if (!GameController.map.takenTiles.ContainsValue(GameController.map.takenTiles[this]))
        {
            return null;
        }
        else
        {
            foreach (StaticObject s in GameController.map.takenTiles.Keys)
            {
                if (GameController.map.takenTiles[s] == GameController.map.takenTiles[this] && s.GetType() == typeof(PickupInventory))
                {
                    return s.gameObject;
                }
            }
        }
        return null;
    }
}