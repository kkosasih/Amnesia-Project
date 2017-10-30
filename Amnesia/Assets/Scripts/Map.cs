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
        string[] data = Resources.Load<TextAsset>("Maps/" + path).text.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None);
        width = data[0].Split(' ').Length;
        height = data.Length;
        tiles = new List<GameObject>(width * height);
        for (int i = 0; i < data.Length; ++i)
        {
            string[] line = data[i].Split(' ');
            for (int j = 0; j < line.Length; ++j)
            {
                switch (line[j][0])
                {
                    case 'G':
                        tiles.Add((GameObject)Instantiate(Resources.Load("Ground Tile"), transform));
                        break;
                    case 'W':
                        tiles.Add((GameObject)Instantiate(Resources.Load("Wall Tile"), transform));
                        break;
                    case 'E':
                        tiles.Add((GameObject)Instantiate(Resources.Load("Entrance Tile"), transform));
                        tiles[tiles.Count - 1].GetComponent<Entrance>().sceneTo = int.Parse(line[j].Split(',')[1]);
                        tiles[tiles.Count - 1].GetComponent<Entrance>().tileTo = int.Parse(line[j].Split(',')[2]);
                        break;
                }
                tiles[tiles.Count - 1].GetComponent<Tile>().UpdatePosition(new Vector2(j, -i));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Return the position of the tile above pos if applicable
    public int TileAbove (int pos)
    {
        return Mathf.Max(pos - width, pos % width);
    }

    // Return the position of the tile below pos if applicable
    public int TileBelow (int pos)
    {
        return Mathf.Min(pos + width, (height - 1) * width + pos % width);
    }

    // Return the position of the tile to the left of pos if applicable
    public int TileLeft (int pos)
    {
        return Mathf.Max(pos - 1, pos / width * width);
    }

    // Return the position of the tile to the right of pos if applicable
    public int TileRight (int pos)
    {
        return Mathf.Min(pos + 1, (pos / width + 1) * width - 1);
    }
}
