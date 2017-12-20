using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : DialoguePart {
    private Character charToMove;
    private List<MoveOptions> moveTo;

    // Constructor taking in the time it takes to perform the action
    public MoveChar (float t1, float t2, Character toMove, List<int> mt) : base(t1, t2)
    {
        charToMove = toMove;
        moveTo = mt;
    }

    // Wait for time1, path the character to the position, then wait for time2
    public override IEnumerator PerformPart()
    {
        yield return new WaitForSeconds(time1);
        charToMove.AutoMove(moveTo);
        yield return new WaitForSeconds(time2);
    }
}
