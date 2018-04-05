using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour {
    #region Attributes
    public bool solid;                      // Whether the object can be gone through
    public bool onMap = false;              // Whether the character is placed on the map  
    public MapNode node;                    // The current node that the character is in
    //public int currentTile;                 // The tile number that the character is on
    public int startTile;                   // The tile to start on
    #endregion

    #region Event Functions
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion

    #region Methods
    // Kill this character
    public virtual void Die ()
    {
        GameController.map.takenTiles.Remove(this);
        Destroy(gameObject);
    }

    // Find the tile on the map to go to
    public void PlaceOnMap ()
    {
        if (GameController.map != null)
        {
            GameController.map.takenTiles.Add(this, startTile);
            transform.position = GameController.map.tiles[GameController.map.takenTiles[this]].transform.position;
            node = GameController.map.NodeTileIn(GameController.map.takenTiles[this]);
            onMap = true;
        }
    }

    // Find the tile on a given map to go to
    public void PlaceOnMap (Map map)
    {
        if (map != null)
        {
            map.takenTiles.Add(this, startTile);
            transform.position = map.tiles[map.takenTiles[this]].transform.position;
            node = map.NodeTileIn(map.takenTiles[this]);
            onMap = true;
        }
    }

    // Get the horizontal distance from a tile; negative is tile to right, positive is tile to left
    protected int HoriDistance (int tile)
    {
        return GameController.map.takenTiles[this] % GameController.map.width - tile % GameController.map.width;
    }

    // Get the vertical distance from a tile; negative is tile below, positive is tile above
    protected int VertDistance (int tile)
    {
        return GameController.map.takenTiles[this] / GameController.map.width - tile / GameController.map.width;
    }

    // Get the combined distance from a tile; always returns positive
    public int TotalDistance (int tile)
    {
        return Mathf.Abs(HoriDistance(tile)) + Mathf.Abs(VertDistance(tile));
    }

    // Get the direction to move in towards a certain tile
    protected Direction DirectionToward (int destination)
    {
        int hDis = HoriDistance(destination);
        int vDis = VertDistance(destination);
        if (Mathf.Abs(vDis) > Mathf.Abs(hDis) && vDis > 0)
        {
            return Direction.Up;
        }
        else if (Mathf.Abs(vDis) > Mathf.Abs(hDis) && vDis < 0)
        {
            return Direction.Down;
        }
        else if (Mathf.Abs(hDis) >= Mathf.Abs(vDis) && hDis > 0)
        {
            return Direction.Left;
        }
        else if (Mathf.Abs(hDis) >= Mathf.Abs(vDis) && hDis < 0)
        {
            return Direction.Right;
        }
        else
        {
            return Direction.Invalid;
        }
    }
    #endregion
}
