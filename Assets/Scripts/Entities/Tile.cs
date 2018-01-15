using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public static bool debug = false;
    private GameObject debugText;
    public TileType type;
    public TileType startType;
    public Vector2 position;
    public List<Attack> attacks;

    // Use this for initialization
    void Start ()
    {
        startType = type;
        UpdatePosition(position);
        attacks = new List<Attack>();
        if (debug)
        {
            debugText = Instantiate(Resources.Load<GameObject>("GUI/DebugText"), GameObject.Find("DynamicCanvas").transform);
            debugText.GetComponent<UITracking>().obj = gameObject;
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (debug)
        {
            debugText.GetComponent<Text>().text = attacks.Count.ToString();
        }
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

public enum TileType
{
    Ground,
    Wall,
    Entrance,
    Shop,
    Sign,
    Pickup
}