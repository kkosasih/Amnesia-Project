using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour {
    public TileType type;                            
    public Vector2 position;
    public List<Attack> attacks;
    private SpriteRenderer _spriteRenderer;     

    void Awake ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
        UpdatePosition(position);
    }
    
    // Update is called once per frame
    void Update ()
    {
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