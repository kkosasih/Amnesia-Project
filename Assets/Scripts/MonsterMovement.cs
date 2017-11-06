using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {
    public PlayerController pc;
    public int currentTile;
    public float moveDelay;
    private float delayLeft;

    public void MoveMonster(int moveTo)
    {
        switch (pc.map.tiles[moveTo].GetComponent<Tile>().type)
        {
            case Tile.TileType.Ground:
                currentTile = moveTo;
                transform.position = pc.map.tiles[currentTile].transform.position;
                break;
            case Tile.TileType.Wall:
                break;
            case Tile.TileType.Entrance:
                break;
            case Tile.TileType.Shop:
                break;
        }
    }
    // Use this for initialization
    void Start ()
    {
        delayLeft = moveDelay;
        MoveMonster(currentTile);
	}
	
	// Update is called once per frame
	void Update () {
        int horizontalDistance = pc.currentTile % pc.map.width - currentTile % pc.map.width;
        int verticalDistance = pc.currentTile / pc.map.width - currentTile / pc.map.width;
        if (delayLeft > 0.0f)
        {
            delayLeft -= Time.deltaTime;
        }
        else
        {
            delayLeft = moveDelay;
            if (Mathf.Abs(verticalDistance) >= Mathf.Abs(horizontalDistance) && verticalDistance < 0)
            {
                MoveMonster(pc.map.TileAbove(currentTile));
            }
            else if (Mathf.Abs(verticalDistance) >= Mathf.Abs(horizontalDistance) && verticalDistance > 0)
            {
                MoveMonster(pc.map.TileBelow(currentTile));
            }
            else if (Mathf.Abs(horizontalDistance) > Mathf.Abs(verticalDistance) && horizontalDistance < 0)
            {
                MoveMonster(pc.map.TileLeft(currentTile));
            }
            else if (Mathf.Abs(horizontalDistance) > Mathf.Abs(verticalDistance) && horizontalDistance > 0)
            {
                MoveMonster(pc.map.TileRight(currentTile));
            }
        }
    }
}
