using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
    public int width;
    public int height;
    public List<Tile> tiles;
    public int playerLoc;

    public Map (string data, int start)
    {
        string baseData = data.Replace("|", "");
        width = data.Contains("|") ? data.IndexOf('|') : baseData.Length;
        height = baseData.Length / width;
        tiles = new List<Tile>(baseData.Length);
        for (int i = 0; i < tiles.Capacity; ++i)
        {
            switch (baseData[i])
            {
                case 'G':
                    tiles.Add(new Tile(false, new Vector2((i % width) - (width - 1) / 2.0f, (height - 1) / 2.0f - (i / width))));
                    break;
                case 'W':
                    tiles.Add(new Tile(true, new Vector2((i % width) - (width - 1) / 2.0f, (height - 1) / 2.0f - (i / width))));
                    break;
            }
            
        }
        playerLoc = start;
    }

    public int tileAbove (int pos)
    {
        return Mathf.Max(pos - width, pos % width);
    }

    public int tileBelow (int pos)
    {
        return Mathf.Min(pos + width, (height - 1) * width + pos % width);
    }

    public int tileLeft (int pos)
    {
        return Mathf.Max(pos - 1, pos / width * width);
    }

    public int tileRight (int pos)
    {
        return Mathf.Min(pos + 1, (pos / width + 1) * width - 1);
    }
}
