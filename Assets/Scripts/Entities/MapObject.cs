using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : StaticObject {
    #region Attributes
    [SerializeField]
    private int offset;                     // The user-defined addition to sorting
    private SpriteRenderer[] sprites;       // The sprite to layer
    private MapObjectChild[] children;      // The map object children to use
    #endregion

    #region Properties

    #endregion

    #region Event Functions
    void Awake ()
    {
        children = GetComponentsInChildren<MapObjectChild>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = -(int)transform.position.y + offset;
        }
    }

    // Use this for initialization
    void Start ()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    // Update the sorting layer to account for the player

    #endregion

    #region Methods
    // Kill this character
    public override void Die ()
    {
        foreach (MapObjectChild m in children)
        {
            m.Die();
        }
        base.Die();
    }

    // Place the object on the closest tile
    public virtual void PlaceOnMap ()
    {
        int tile = -(int)transform.position.y * GameController.instance.map.Width + (int)transform.position.x % GameController.instance.map.Width;
        if (GameController.instance.map != null)
        {
            if (tile >= GameController.instance.map.Tiles.Count)
            {
                return;
            }
            if (!GameController.instance.map.TakenTiles.ContainsKey(this))
            {
                GameController.instance.map.TakenTiles.Add(this, tile);
            }
            else
            {
                CurrentTile = tile;
            }
            transform.position = GameController.instance.map.Tiles[tile].transform.position;
            foreach (MapObjectChild m in children)
            {
                m.PlaceOnMap(tile + -(int)m.transform.localPosition.y * GameController.instance.map.Width + (int)m.transform.localPosition.x);
            }
        }
    }

    // Place the object at the point on the map
    public override void PlaceOnMap (int tile)
    {
        if (GameController.instance.map != null)
        {
            if (!GameController.instance.map.TakenTiles.ContainsKey(this))
            {
                GameController.instance.map.TakenTiles.Add(this, tile);
            }
            else
            {
                CurrentTile = tile;
            }
            transform.position = GameController.instance.map.Tiles[tile].transform.position;
            foreach (MapObjectChild m in children)
            {
                m.PlaceOnMap(tile + -(int)m.transform.localPosition.y * GameController.instance.map.Width + -(int)m.transform.localPosition.x);
            }
        }
    }

    // Place the object at the point on a given map
    public override void PlaceOnMap (Map map, int tile)
    {
        if (map != null)
        {
            if (!map.TakenTiles.ContainsKey(this))
            {
                map.TakenTiles.Add(this, tile);
            }
            else
            {
                CurrentTile = tile;
            }
            transform.position = map.Tiles[tile].transform.position;
            foreach (MapObjectChild m in children)
            {
                m.PlaceOnMap(map, tile + -(int)m.transform.localPosition.y * map.Width + -(int)m.transform.localPosition.x);
            }
        }
    }

    // Update the layring to account for player position
    public void UpdateLayering ()
    {
        if (CurrentTile <= PlayerCharacter.instance.CurrentTile)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.sortingLayerID = SortingLayer.NameToID("BelowPlayer");
            }
        }
        else
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.sortingLayerID = SortingLayer.NameToID("AbovePlayer");
            }
        }
    }
    #endregion
}
