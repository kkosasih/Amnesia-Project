using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour {
    public TileType type;                            
    public Vector2 position;                    
    public int attackID = 0;
    public int attackDamage = 0;
    private SpriteRenderer _spriteRenderer;     

    

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        UpdatePosition(position);
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Change position of the tile
    public void UpdatePosition (Vector2 positionP)
    {
        position = positionP;
        transform.localPosition = position;
    }

    // Switch the tile's attack state
    public IEnumerator GiveAttack (int id, int damage, float time)
    {
        attackID = id;
        attackDamage = damage;
        UpdateColor();
        yield return new WaitForSeconds(time);
        attackID = 0;
        attackDamage = 0;
        UpdateColor();
    }

    // Update the color of the tile
    private void UpdateColor ()
    {
        if (attackID != 0)
        {
            _spriteRenderer.color = new Color(1, 0, 0);
        }
        else
        {
            switch (type)
            {
                case TileType.Ground:
                    if (GetComponent<Shop>() != null)
                    {
                        _spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
                    }
                    else
                    {
                        _spriteRenderer.color = new Color(0, 1, 0);
                    }
                    break;
                case TileType.Wall:
                    _spriteRenderer.color = new Color(1, 1, 0);
                    break;
                case TileType.Entrance:
                    _spriteRenderer.color = new Color(0, 0, 1);
                    break;
            }
        }
    }
}

public enum TileType
{
    Ground,
    Wall,
    Entrance,
    Shop
}