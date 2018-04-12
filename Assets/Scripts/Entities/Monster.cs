using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {
    #region Attributes
    //public PlayerCharacter player;  // The player script to track
    //public bool pickuptile = false;
    protected GameObject canvas;
    [SerializeField]
    protected int range;            // The range before player detection
    [SerializeField]
    protected bool dead = false;    // Whether the monster is dead
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
        if (Preventions == 0 && OnMap)
        {
            // Attack player if in ranges
            if (Mathf.Abs(HoriDistance(PlayerCharacter.instance.CurrentTile)) <= 4 && VertDistance(PlayerCharacter.instance.CurrentTile) == 0 ||
                Mathf.Abs(VertDistance(PlayerCharacter.instance.CurrentTile)) <= 4 && HoriDistance(PlayerCharacter.instance.CurrentTile) == 0)
            {
                StartCoroutine(Attack(DirectionToward(PlayerCharacter.instance.CurrentTile)));
            }
            else if (lastMove >= delay && !moving)
            {
                // Move to player if detected
                if (TotalDistance(PlayerCharacter.instance.CurrentTile) <= range)
                {
                    StartCoroutine(AutoMoveOneStep(PlayerCharacter.instance.CurrentTile));
                }
                // Move randomly if not detected
                else
                {
                    Move((Direction)Random.Range(0, 4));
                }
            }
        }
        // Die after animation
        if (dead)
        {
            DropItem();
        }
        base.Update();
    }
    #endregion

    #region Methods
    // Kill this character
    public override void Die ()
    {
        canvas.GetComponent<QuestTracking>().questobj(name);
        SetAllBools("dead", true);
    }

    // Delete the character and drop an item
    protected void DropItem ()
    {
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
    public override IEnumerator Attack (Direction dir)
    {
        ++movementPreventions;
        SetAllIntegers("direction", (int)dir);
        StartCoroutine(Helper.PlayInTime(animators, "attacking", true, false, 1.5f));
        yield return new WaitForSeconds(0.5f);
        AttackController.instance.StraightAttack(new Attack(teamID, 5, 0.2f), dir, CurrentTile, 4, 1, 5f);
        yield return new WaitForSeconds(1);
        --movementPreventions;
    }
    #endregion
}