using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLooks : MonoBehaviour {
    public List<Looks> heads = new List<Looks>();
    public List<Looks> clothes = new List<Looks>();
    public List<Looks> shoes = new List<Looks>();
    public int x = 0, y = 0, z = 0;
    public Text text1, text2, text3;
    public Looks headc, clothec, shoec;

    void Start()
    {
        heads.Add(new Looks("heads1"));
        clothes.Add(new Looks("clothes1"));
        shoes.Add(new Looks("shoes1"));

        text1.text = heads[0].itemName;
        text2.text = clothes[0].itemName;
        text3.text = shoes[0].itemName;
    }

    public void head(int a)
    {
        x += a;
        text1.text = heads[x].itemName;
        headc = heads[x];
    }

    public void clothe(int b)
    {
        y += b;
        text2.text = clothes[y].itemName;
        clothec = clothes[y];
    }

    public void shoe(int c)
    {
        z += c;
        text3.text = shoes[z].itemName;
        shoec = shoes[z];
    }

    public void save()
    {

    }
}
