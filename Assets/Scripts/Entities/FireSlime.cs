using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireSlime : Monster {
    #region Attributes

    #endregion

    #region Properties

    #endregion

    #region Event Functions
    // Update is called once per frame
    protected override void Update ()
    {
        // If not in a cutscene
        if (health > 0 && Preventions == 0 && OnMap)
        {
            // Attack player if in range
            if (Mathf.Max(Mathf.Abs(HoriDistance(PlayerCharacter.instance.CurrentTile)), Mathf.Abs(VertDistance(PlayerCharacter.instance.CurrentTile))) <= 2)
            {
                StartCoroutine(Attack(DirectionToward(PlayerCharacter.instance.CurrentTile)));
            }
        }
        base.Update();
    }
    #endregion

    #region Methods

    #endregion

    #region Coroutines
    // Attack in a given direction dir
    public override IEnumerator Attack (Direction dir)
    {
        ++movementPreventions;
        SetAllIntegers("direction", (int)dir);
        StartCoroutine(Helper.PlayInTime(animators, "attacking", true, false, 2f));
        yield return new WaitForSeconds(1f);
        AttackController.instance.BurstAttack(new Attack(teamID, 10, 1f), CurrentTile, 2, 1f);
        yield return new WaitForSeconds(1f);
        --movementPreventions;
    }
    #endregion
}
