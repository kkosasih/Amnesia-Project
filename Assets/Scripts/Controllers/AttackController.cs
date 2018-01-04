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
        for (int i = 0; i < length; ++i)
        {
            int baseTile = -1;
            switch (dir)
            {
                case Direction.Up:
                    baseTile = GameController.map.TileAboveStrict(tile, i + 1);
                    break;
                case Direction.Down:
                    baseTile = GameController.map.TileBelowStrict(tile, i + 1);
                    break;
                case Direction.Left:
                    baseTile = GameController.map.TileLeftStrict(tile, i + 1);
                    break;
                case Direction.Right:
                    baseTile = GameController.map.TileRightStrict(tile, i + 1);
                    break;
            }
            int tileToHit = baseTile;
            if (tileToHit >= 0)
            {
                StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                if (i == 0)
                {
                    Projectile p = Instantiate(Resources.Load<GameObject>("Attacks/Arrow"), GameController.map.tiles[tileToHit].transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity).GetComponent<Projectile>();
                    p.direction = dir;
                    p.speed = speed;
                    p.duration = (length - 1) / speed;
                }
            }
            for (int j = 1; j < width; ++j)
            {
                if (dir == Direction.Up || dir == Direction.Down)
                {
                    tileToHit = GameController.map.TileLeftStrict(baseTile, j);
                }
                else
                {
                    tileToHit = GameController.map.TileAboveStrict(baseTile, j);
                }
                if (tileToHit >= 0)
                {
                    StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                    if (i == 0)
                    {
                        Projectile p = Instantiate(Resources.Load<GameObject>("Attacks/Arrow"), GameController.map.tiles[tileToHit].transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity).GetComponent<Projectile>();
                        p.direction = dir;
                        p.speed = speed;
                        p.duration = (length - 1) / speed;
                    }
                }
                if (dir == Direction.Up || dir == Direction.Down)
                {
                    tileToHit = GameController.map.TileRightStrict(baseTile, j);
                }
                else
                {
                    tileToHit = GameController.map.TileBelowStrict(baseTile, j);
                }
                if (tileToHit >= 0)
                {
                    StartCoroutine(GiveAttack(tileToHit, a, i / speed));
                    if (i == 0)
                    {
                        Projectile p = Instantiate(Resources.Load<GameObject>("Attacks/Arrow"), GameController.map.tiles[tileToHit].transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity).GetComponent<Projectile>();
                        p.direction = dir;
                        p.speed = speed;
                        p.duration = (length - 1) / speed;
                    }
                }
            }
        }
    }

    // Template for a burst attack
    public void BurstAttack (Attack a, int tile, int length, float speed)
    {
        int baseTile = GameController.map.TileLeftStrict(GameController.map.TileAboveStrict(tile, length), length);
        for (int j = 0; j <= length * 2; ++j)
        {
            for (int k = 0; k <= length * 2; ++k)
            {
                int tileToHit = GameController.map.TileRightStrict(GameController.map.TileBelowStrict(baseTile, j), k);
                if (tileToHit >= 0)
                {
                    StartCoroutine(GiveAttack(tileToHit, a, Mathf.Max(Mathf.Abs(j - length), Mathf.Abs(k - length)) / speed));
                }
            }
        }
        GameObject g = Instantiate(Resources.Load<GameObject>("Attacks/FireWalls"), GameController.map.tiles[tile].transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity);
        g.GetComponent<Projectile>().duration = length / speed;
        foreach (Projectile p in g.GetComponentsInChildren<Projectile>())
        {
            p.speed = speed;
            p.duration = length / speed;
        }
        g.GetComponent<Projectile>().speed = 0;
        foreach (Expansion e in g.GetComponentsInChildren<Expansion>())
        {
            e.speed = speed * 2;
        }
    }

    // Give a delay on an attack
    private static IEnumerator GiveAttack (int tile, Attack a, float before)
    {
        yield return new WaitForSeconds(before);
        GameController.map.tiles[tile].GetComponent<Tile>().attacks.Add(a);
    }
}

