using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour {
    #region Attributes
    public int startTile;   // The tile to start on
    [SerializeField]
    protected bool solid;   // Whether the object can be gone through
    //protected bool onMap = false;   // Whether the character is placed on the map  
    //protected MapNode node;         // The current node that the character is in
    //public int currentTile;                 // The tile number that the character is on
    #endregion

    #region Properties
    // Returns solid
    public bool Solid
    {
        get
        {
            return solid;
        }
    }

    // Returns the tile that the object is currently at
    public int CurrentTile
    {
        get
        {
            if (!OnMap)
            {
                return -1;
            }
            else
            {
                return GameController.instance.map.TakenTiles[this];
            }
        }
        protected set
        {
            if (OnMap)
            {
                GameController.instance.map.TakenTiles[this] = value;
            }
        }
    }

    // Returns whether the object is on the map
    public bool OnMap
    {
        get
        {
            return GameController.instance.map != null && GameController.instance.map.TakenTiles != null && GameController.instance.map.TakenTiles.ContainsKey(this);
        }
    }

    // Returns the node the object is located in
    public MapNode Node
    {
        get
        {
            if (OnMap)
            {
                return GameController.instance.map.NodeTileIn(CurrentTile);
            }
            else
            {
                return null;
            }
        }
    }
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
        GameController.instance.map.TakenTiles.Remove(this);
        Destroy(gameObject);
    }

    // Place the object at the point on the map
    public void PlaceOnMap (int tile)
    {
        if (GameController.instance.map != null)
        {
            GameController.instance.map.TakenTiles.Add(this, tile);
            transform.position = GameController.instance.map.Tiles[tile].transform.position;
        }
    }

    // Place the object at the point on a given map
    public void PlaceOnMap (Map map, int tile)
    {
        if (map != null)
        {
            map.TakenTiles.Add(this, tile);
            transform.position = map.Tiles[tile].transform.position;
        }
    }

    // Get the horizontal distance from a tile; negative is tile to right, positive is tile to left
    protected int HoriDistance (int tile)
    {
        return CurrentTile % GameController.instance.map.Width - tile % GameController.instance.map.Width;
    }

    // Get the vertical distance from a tile; negative is tile below, positive is tile above
    protected int VertDistance (int tile)
    {
        return CurrentTile / GameController.instance.map.Width - tile / GameController.instance.map.Width;
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
