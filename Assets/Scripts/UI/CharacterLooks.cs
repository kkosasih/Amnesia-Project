using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLooks : MonoBehaviour {
    public List<Sprite> heads = new List<Sprite>();
    public List<Sprite> clothes = new List<Sprite>();
    public List<Sprite> shoes = new List<Sprite>();
    public int x = 0, y = 0, z = 0;
    public Text text1, text2, text3;
    public Sprite headc, clothec, shoec;

    void Start()
    {
        //heads.Add(new Looks("heads1"));
        //clothes.Add(new Looks("clothes1"));
        
        //shoes.Add(new Looks("shoes1"));

        text1.text = heads[0].name;
        text2.text = clothes[0].name;
        text3.text = shoes[0].name;
    }

    public void head(int a)
    {
        x += a;
        text1.text = heads[x].name;
        headc = heads[x];
    }

    public void clothe(int b)
    {
        y += b;
        text2.text = clothes[y].name;
        clothec = clothes[y];
    }

    public void shoe(int c)
    {
        z += c;
        text3.text = shoes[z].name;
        shoec = shoes[z];
    }

    public void save()
    {

    }
}
