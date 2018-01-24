using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public static bool debug = false;   // Whether the tiles show debug data
    private GameObject debugText;       // The text put for tiles during debug
    public TileType type;               // The type of the tile
    public TileType startType;          // The beginning type of tile (to revert to it later)
    public Vector2 position;            // The position of the tile in the world
    public List<Attack> attacks;        // The attack values that are stored in the tile

    // Use this for initialization
    void Start ()
    {
        // Initialize tile
        startType = type;
        UpdatePosition(position);
        attacks = new List<Attack>();
        // Debugging
        if (debug)
        {
            debugText = Instantiate(Resources.Load<GameObject>("GUI/DebugText"), GameObject.Find("DynamicCanvas").transform);
            debugText.GetComponent<UITracking>().obj = gameObject;
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
        // Debugging
        if (debug)
        {
            debugText.GetComponent<Text>().text = attacks.Count.ToString();
        }
        // Update the attacks in the tile
        for (int i = 0; i < attacks.Count;)
        {
            Attack a = new Attack(attacks[i]);
            a.duration -= Time.deltaTime;
            if (a.duration >= 0)
            {
                attacks[i] = a;
                ++i;
            }
            else
            {
                attacks.RemoveAt(i);
            }
        }
    }

    // Change position of the tile
    public void UpdatePosition (Vector2 positionP)
    {
        position = positionP;
        transform.localPosition = position;
    }

    // See how much damage would be done to a given team
    public int Damage (int team)
    {
        int result = 0;
        // Iterate through attacks to find most damaging one
        foreach (Attack a in attacks)
        {
            if (a.id != team)
            {
                result = Mathf.Max(result, a.damage);
            }
        }
        return result;
    }
}

// The tile types
public enum TileType
{
    Ground,
    Wall,
    Entrance,
    Shop,
    Sign,
    Pickup
}