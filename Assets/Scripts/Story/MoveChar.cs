using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : DialoguePart {
    private Character charToMove;
    private List<Direction> moveTo;

    // Change the variables based on a string
    public override void ChangeSettings (string data)
    {
        base.ChangeSettings(data);
        string[] parameters = data.Split('|');
        charToMove = GameObject.Find(parameters[2]).GetComponent<Character>();
        moveTo = new List<Direction>();
        foreach (string s in parameters[3].Split(','))
        {
            switch (s)
            {
                case "U":
                    moveTo.Add(Direction.Up);
                    break;
                case "D":
                    moveTo.Add(Direction.Down);
                    break;
                case "L":
                    moveTo.Add(Direction.Left);
                    break;
                case "R":
                    moveTo.Add(Direction.Right);
                    break;
            }
        }
    }

    // Wait for time1, path the character to the position, then wait for time2
    public override IEnumerator PerformPart()
    {
        isRunning = true;
        yield return new WaitForSeconds(time1);
        yield return StartCoroutine(charToMove.AutoMove(moveTo));
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}
