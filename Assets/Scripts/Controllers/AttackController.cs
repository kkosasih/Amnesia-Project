using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    public static AttackController instance;

    // Use this for initialization
    void Start ()
    {
        instance = this;
    }

    // Template for a straight-line attack
    public void StraightAttack (Attack a, Direction dir, int tile, int length, int width, float speed)
    {
        switch (dir)
        {
            case Direction.Up:
                for (int i = 0; i < length; ++i)
                {
                    int baseTile = GameController.map.TileAboveStrict(tile, i + 1);
                    int tileToHit = baseTile;
                    if (tileToHit >= 0)
                    {
                        StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                    }
                    for (int j = 1; j < width; ++j)
                    {
                        tileToHit = GameController.map.TileLeftStrict(baseTile, j);
                        if (tileToHit >= 0)
                        {
                            StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                        }
                        tileToHit = GameController.map.TileRightStrict(baseTile, j);
                        if (tileToHit >= 0)
                        {
                            StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                        }
                    }
                }
                break;
            case Direction.Down:
                for (int i = 0; i < length; ++i)
                {
                    int baseTile = GameController.map.TileBelowStrict(tile, i + 1);
                    int tileToHit = baseTile;
                    if (tileToHit >= 0)
                    {
                        StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                    }
                    for (int j = 1; j < width; ++j)
                    {
                        tileToHit = GameController.map.TileLeftStrict(baseTile, j);
                        if (tileToHit >= 0)
                        {
                            StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                        }
                        tileToHit = GameController.map.TileRightStrict(baseTile, j);
                        if (tileToHit >= 0)
                        {
                            StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                        }
                    }
                }
                break;
            case Direction.Left:
                for (int i = 0; i < length; ++i)
                {
                    int baseTile = GameController.map.TileLeftStrict(tile, i + 1);
                    int tileToHit = baseTile;
                    if (tileToHit >= 0)
                    {
                        StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                    }
                    for (int j = 1; j < width; ++j)
                    {
                        tileToHit = GameController.map.TileAboveStrict(baseTile, j);
                        if (tileToHit >= 0)
                        {
                            StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                        }
                        tileToHit = GameController.map.TileBelowStrict(baseTile, j);
                        if (tileToHit >= 0)
                        {
                            StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                        }
                    }
                }
                break;
            case Direction.Right:
                for (int i = 0; i < length; ++i)
                {
                    int baseTile = GameController.map.TileRightStrict(tile, i + 1);
                    int tileToHit = baseTile;
                    if (tileToHit >= 0)
                    {
                        StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                    }
                    for (int j = 1; j < width; ++j)
                    {
                        tileToHit = GameController.map.TileAboveStrict(baseTile, j);
                        if (tileToHit >= 0)
                        {
                            StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                        }
                        tileToHit = GameController.map.TileBelowStrict(baseTile, j);
                        if (tileToHit >= 0)
                        {
                            StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                        }
                    }
                }
                break;
        }
        int baseProjTile = GameController.map.TileRightStrict(tile);
        int projTile = baseProjTile;
        if (projTile >= 0)
        {
            Projectile p = Instantiate(Resources.Load<GameObject>("Attacks/Arrow"), GameController.map.tiles[projTile].transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity).GetComponent<Projectile>();
            p.direction = dir;
            p.speed = speed;
            p.duration = length / speed;
        }
        for (int j = 1; j < width; ++j)
        {
            projTile = GameController.map.TileAboveStrict(baseProjTile, j);
            Projectile p = Instantiate(Resources.Load<GameObject>("Attacks/Arrow"), GameController.map.tiles[GameController.map.TileRight(tile)].transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity).GetComponent<Projectile>();
            p.direction = dir;
            p.speed = speed;
            p.duration = length / speed;
            projTile = GameController.map.TileBelowStrict(baseProjTile, j);
            p = Instantiate(Resources.Load<GameObject>("Attacks/Arrow"), GameController.map.tiles[GameController.map.TileRight(tile)].transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity).GetComponent<Projectile>();
            p.direction = dir;
            p.speed = speed;
            p.duration = length / speed;
        }
    }

    // Template for a burst attack
    public void BurstAttack (Attack a, int tile, int length, float speed)
    {
        StartCoroutine(GiveAttack(tile, a, 0));
        for (int i = 0; i < length; ++i)
        {
            int tileToHit = GameController.map.TileRightStrict(tile, i + 1);
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, (i + 1) / speed));
            }
            tileToHit = GameController.map.TileRightStrict(GameController.map.TileAboveStrict(tile, i + 1), i + 1);
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, (i + 1) / speed));
            }
            tileToHit = GameController.map.TileAboveStrict(tile, i + 1);
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, (i + 1) / speed));
            }
            tileToHit = GameController.map.TileLeftStrict(GameController.map.TileAboveStrict(tile, i + 1), i + 1);
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, (i + 1) / speed));
            }
            tileToHit = GameController.map.TileLeftStrict(tile, i + 1);
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, (i + 1) / speed));
            }
            tileToHit = GameController.map.TileLeftStrict(GameController.map.TileBelowStrict(tile, i + 1), i + 1);
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, (i + 1) / speed));
            }
            tileToHit = GameController.map.TileBelowStrict(tile, i + 1);
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, (i + 1) / speed));
            }
            tileToHit = GameController.map.TileRightStrict(GameController.map.TileBelowStrict(tile, i + 1), i + 1);
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, (i + 1) / speed));
            }
        }
        GameObject g = Instantiate(Resources.Load<GameObject>("Attacks/FireWalls"), GameController.map.tiles[tile].transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity);
        foreach (Projectile p in g.GetComponentsInChildren<Projectile>())
        {
            p.speed = speed;
            p.duration = length / speed;
        }
        foreach (Expansion e in g.GetComponentsInChildren<Expansion>())
        {
            e.speed = speed;
        }
    }

    // Give a delay on an attack
    private static IEnumerator GiveAttack (int tile, Attack a, float before)
    {
        yield return new WaitForSeconds(before);
        GameController.map.tiles[tile].GetComponent<Tile>().attacks.Add(a);
    }
}

