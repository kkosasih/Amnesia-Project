using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour {
    public string data;
    public int width;
    public int height;
    public List<GameObject> tiles;
    public int playerLoc;

    // Use this for initialization
    void Start()
    {
        string baseData = data.Replace("|", "");
        width = data.Contains("|") ? data.IndexOf('|') : baseData.Length;
        height = baseData.Length / width;
        tiles = new List<GameObject>(baseData.Length);
        for (int i = 0; i < tiles.Capacity; ++i)
        {
            switch (baseData[i])
            {
                case 'G':
                    tiles.Add(Instantiate((GameObject)Resources.Load("Ground Tile"), transform));
                    break;
                case 'W':
                    tiles.Add(Instantiate((GameObject)Resources.Load("Wall Tile"), transform));
                    break;
                case 'E':
                    tiles.Add(Instantiate((GameObject)Resources.Load("Entrance Tile"), transform));
                    break;
            }
            tiles[i].GetComponent<Tile>().UpdatePosition(new Vector2(i % width, -i / width));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int TileAbove(int pos)
    {
        return Mathf.Max(pos - width, pos % width);
    }

    public int TileBelow(int pos)
    {
        return Mathf.Min(pos + width, (height - 1) * width + pos % width);
    }

    public int TileLeft(int pos)
    {
        return Mathf.Max(pos - 1, pos / width * width);
    }

    public int TileRight(int pos)
    {
        return Mathf.Min(pos + 1, (pos / width + 1) * width - 1);
    }
}
