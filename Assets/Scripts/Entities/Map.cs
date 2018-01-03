using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour {
    public string path;
    public int width;
    public int height;
    public List<GameObject> tiles;

    // Use this for initialization
    void Start()
    {
        string[] data = Resources.Load<TextAsset>("Maps/" + path).text.Split(new string[] { "..." }, System.StringSplitOptions.None);
        width = data[0].Split(' ').Length;
        height = data.Length;
        tiles = new List<GameObject>(width * height);
        for (int i = 0; i < data.Length; ++i)
        {
            string[] line = data[i].Trim().Split(' ');
            for (int j = 0; j < line.Length; ++j)
            {
                switch (line[j][0])
                {
                    case 'G':
                        tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Ground Tile"), transform));
                        break;
                    case 'W':
                        tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Wall Tile"), transform));
                        break;
                    case 'E':
                        tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Entrance Tile"), transform));
                        Entrance e = tiles[tiles.Count - 1].GetComponent<Entrance>();
                        string[] args = line[j].Split(',');
                        e.sceneTo = int.Parse(args[1]);
                        e.tileFrom = int.Parse(args[2]);
                        e.moveTo = (Direction)int.Parse(args[3]);
                        break;
                    case 'S':
                        tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Shop Tile"), transform));
                        tiles[tiles.Count - 1].GetComponent<Shop>().numOfItems = int.Parse(line[j].Split(',')[1]);
                        break;
                    case 'N':
                        tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Sign Tile"), transform));
                        tiles[tiles.Count - 1].GetComponent<Sign>().path = line[j].Split(',')[1];
                        break;
                }
                tiles[tiles.Count - 1].GetComponent<Tile>().UpdatePosition(new Vector2(j, -i));
            }
        }
        transform.Find("MapSprite").transform.position = new Vector3((float)width / 2 - 0.5f, (float)-height / 2 + 0.5f, 0);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    // Return the position of the tile above pos if applicable
    public int TileAbove (int pos)
    {
        return Mathf.Max(pos - width, pos % width);
    }

    // Return the position of the tile above pos if possible
    public int TileAboveStrict (int pos)
    {
        return pos < 0 || pos - width < pos % width ? -1 : pos - width;
    }

    // Return the position of the tile dis number of tiles above pos if applicable
    public int TileAboveStrict (int pos, int dis)
    {
        return pos < 0 || pos - dis * width < pos % width ? -1 : pos - dis * width;
    }

    // Return the position of the tile below pos if applicable
    public int TileBelow (int pos)
    {
        return Mathf.Min(pos + width, (height - 1) * width + pos % width);
    }

    // Return the position of the tile below pos if possible
    public int TileBelowStrict (int pos)
    {
        return pos < 0 || pos + width > (height - 1) * width + pos % width ? -1 : pos + width;
    }

    // Return the position of the tile dis number of tiles below pos if applicable
    public int TileBelowStrict (int pos, int dis)
    {
        return pos < 0 || pos + dis * width > (height - 1) * width + pos % width ? -1 : pos + dis * width;
    }

    // Return the position of the tile to the left of pos if applicable
    public int TileLeft (int pos)
    {
        return Mathf.Max(pos - 1, pos / width * width);
    }

    // Return the position of the tile to the left of pos if possible
    public int TileLeftStrict (int pos)
    {
        return pos < 0 || pos - 1 < pos / width * width ? -1 : pos - 1;
    }

    // Return the position of the tile dis number of tiles to the left of pos if applicable
    public int TileLeftStrict (int pos, int dis)
    {
        return pos < 0 || pos - dis < pos / width * width ? -1 : pos - dis;
    }

    // Return the position of the tile to the right of pos if applicable
    public int TileRight (int pos)
    {
        return Mathf.Min(pos + 1, (pos / width + 1) * width - 1);
    }

    // Return the position of the tile to the right of pos if possible
    public int TileRightStrict (int pos)
    {
        return pos < 0 || pos + 1 > (pos / width + 1) * width - 1 ? -1 : pos + 1;
    }

    // Return the position of the tile dis number of tiles to the right of pos if applicable
    public int TileRightStrict (int pos, int dis)
    {
        return pos < 0 || pos + dis > (pos / width + 1) * width - dis ? -1 : pos + dis;
    }
}
