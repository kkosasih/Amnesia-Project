using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {
    #region Attributes
    //public PlayerCharacter player;  // The player script to track
    //public bool pickuptile = false;
    protected GameObject canvas;
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    protected override void Update ()
    {
        canvas = GameObject.Find("Canvas");
        // If not in a cutscene
        if (DialogueController.instance.MovementPreventions + movementPreventions == 0 && OnMap)
        {
            if (Mathf.Max(Mathf.Abs(HoriDistance(PlayerCharacter.instance.CurrentTile)), Mathf.Abs(VertDistance(PlayerCharacter.instance.CurrentTile))) <= 2)
            {
                StartCoroutine(Attack(Direction.Up));
            }
            else if (lastMove >= delay && !moving)
            {
                StartCoroutine(AutoMoveOneStep(PlayerCharacter.instance.CurrentTile));
            }
        }
        base.Update();
    }
    #endregion

    #region Methods
    // Kill this character and drop an item
    public override void Die ()
    {
        Debug.Log(CurrentTile);
        canvas.GetComponent<QuestTracking>().questobj(name);
        GameObject drop = GameController.instance.map.ObjectTakingTile(CurrentTile);
        // If the tile isn't already a pickup tile
        if (drop == null || drop.GetComponent<PickupInventory>() == null)
        {
            drop = Instantiate(Resources.Load<GameObject>("ItemDrop"));
            StaticObject s = drop.GetComponent<StaticObject>();
            s.PlaceOnMap(CurrentTile);
            drop.GetComponent<PickupInventory>().SetSize(4);
        }
        // Add items here
        drop.GetComponent<PickupInventory>().AddItemByID(2);
        base.Die();
    }
    #endregion

    #region Coroutines
    // Attack in a given direction dir
    public override IEnumerator Attack(Direction dir)
    {
        ++movementPreventions;
        yield return new WaitForSeconds(1);
        AttackController.instance.BurstAttack(new Attack(teamID, 1, 0.5f), CurrentTile, 2, 2);
        yield return new WaitForSeconds(0.5f);
        --movementPreventions;
    }
    #endregion
}