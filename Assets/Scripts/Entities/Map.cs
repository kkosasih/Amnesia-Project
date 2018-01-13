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
        Texture2D mapLayout = Resources.Load<Texture2D>("Maps/Textures/" + path);
        List<Color> data = Helper.ReverseInChunks(new List<Color>(mapLayout.GetPixels()), mapLayout.width);
        string[] entries = Resources.Load<TextAsset>("Maps/Tiles/" + path).text.Split(new string[] { "..." }, System.StringSplitOptions.None);
        Debug.Log(entries.Length);
        width = mapLayout.width;
        height = mapLayout.height;
        tiles = new List<GameObject>(width * height);
        List<int> entrances = new List<int>();
        List<int> shops = new List<int>();
        List<int> signs = new List<int>();
        for (int i = 0; i < data.Count; ++i)
        {
            if (data[i] == new Color(0, 1, 0))
            {
                tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Ground Tile"), transform));
            }
            else if (data[i] == new Color(1, 0, 0))
            {
                tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Wall Tile"), transform));
            }
            else if (data[i] == new Color(0, 0, 1))
            {
                tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Entrance Tile"), transform));
                entrances.Add(i);
            }
            else if (data[i] == new Color(1, 1, 0))
            {
                tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Shop Tile"), transform));
                shops.Add(i);
            } 
            else if (data[i] == new Color(1, 0, 1))
            {
                tiles.Add((GameObject)Instantiate(Resources.Load("Tiles/Sign Tile"), transform));
                signs.Add(i);
            }     
            else
            {
                Debug.Log("Tile at (" + (i % width).ToString() + ", " + (i / width).ToString() + ") is not valid. Color is " + data[i].ToString());
            }
            tiles[i].GetComponent<Tile>().UpdatePosition(new Vector2(i % width, -i / width));
        }
        int entrancesI = 0, shopsI = 0, signsI = 0;
        for (int i = 0; i < entries.Length; ++i)
        {
            switch (entries[i].Trim()[0])
            {
                case 'E':
                    Entrance e = tiles[entrances[entrancesI++]].GetComponent<Entrance>();
                    string[] args = entries[i].Split(':')[1].Split(',');
                    e.sceneTo = int.Parse(args[0]);
                    e.tileFrom = int.Parse(args[1]);
                    e.moveTo = (Direction)int.Parse(args[2]);
                    break;
                case 'S':
                    tiles[shops[shopsI++]].GetComponent<Shop>().numOfItems = int.Parse(entries[i].Split(':')[1]);
                    break;
                case 'N':
                    tiles[signs[signsI++]].GetComponent<Sign>().path = entries[i].Split(':')[1];
                    break;
                default:
                    Debug.Log("Invalid entry type");
                    break;
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
