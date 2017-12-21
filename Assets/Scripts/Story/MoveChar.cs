using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : DialoguePart {
    private Character charToMove;
    private List<MoveOptions> moveTo;

    // Constructor taking in the time it takes to perform the action
    public MoveChar (float t1, float t2, Character toMove, List<MoveOptions> mt) : base(t1, t2)
    {
        charToMove = toMove;
        moveTo = mt;
    }

    // Constructor taking in a string
    public MoveChar (string data) : base(data)
    {
        string[] parameters = data.Split('|');
        charToMove = GameObject.Find(parameters[2]).GetComponent<Character>();
        moveTo = new List<MoveOptions>();
        foreach (string s in parameters[3].Split(','))
        {
            switch (s)
            {
                case "U":
                    moveTo.Add(MoveOptions.Up);
                    break;
                case "D":
                    moveTo.Add(MoveOptions.Down);
                    break;
                case "L":
                    moveTo.Add(MoveOptions.Left);
                    break;
                case "R":
                    moveTo.Add(MoveOptions.Right);
                    break;
            }
        }
    }

    // Wait for time1, path the character to the position, then wait for time2
    public override IEnumerator PerformPart()
    {
        isRunning = true;
        yield return new WaitForSeconds(time1);
        charToMove.AutoMove(moveTo);
        yield return new WaitForSeconds(time2);
        isRunning = false;
    }
}
