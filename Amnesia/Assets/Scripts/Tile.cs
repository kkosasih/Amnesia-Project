using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    public bool obstruct;
    public bool hasPlayer;
    public Vector2 position;

    public Tile (bool obstructP, Vector2 positionP)
    {
        obstruct = obstructP;
        position = positionP;
        hasPlayer = false;
    }

    public void setPlayer(bool enter)
    {
        hasPlayer = enter;
    }
}
