using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLooks : MonoBehaviour {
    public List<Sprite> heads = new List<Sprite>();
    public List<Sprite> clothes = new List<Sprite>();
    public List<Sprite> shoes = new List<Sprite>();
    public GameObject character,heado, clotho, shoeo, savemenu, ccmenu, creation;
    public Animator _animator,_animator2,_animator3;
    public int x = 0, y = 0, z = 0, arbitrarynum = 10;
    public Text text1, text2, text3;
    public string headc, clothec, shoec;
    public bool saving = false;

    void Start()
    {
        _animator.SetInteger("Headgear", 1);
        //character = GameObject.Find("Character");
        heado = GameObject.Find("Head");
        clotho = GameObject.Find("Body");
        shoeo = GameObject.Find("Shoe");
        ccmenu = GameObject.Find("Character Creation");

        heads.Add(Resources.Load<Sprite>("Item Icons/Apple"));
        heads.Add(Resources.Load<Sprite>("Item Icons/vest"));
        heads.Add(Resources.Load<Sprite>("CharacterTemp/Prototype_CharacterFront"));

        clothes.Add(Resources.Load<Sprite>("Item Icons/Apple"));
        clothes.Add(Resources.Load<Sprite>("Item Icons/vest"));
        clothes.Add(Resources.Load<Sprite>("CharacterTemp/Prototype_CharacterFront"));

        shoes.Add(Resources.Load<Sprite>("Item Icons/Apple"));
        shoes.Add(Resources.Load<Sprite>("Item Icons/vest"));
        shoes.Add(Resources.Load<Sprite>("CharacterTemp/Prototype_CharacterFront"));

        heado.GetComponent<Image>().sprite = heads[0];
        clotho.GetComponent<Image>().sprite = clothes[0];
        shoeo.GetComponent<Image>().sprite = shoes[0];

        text1.text = heads[0].name;
        text2.text = clothes[0].name;
        text3.text = shoes[0].name;
    }

    public void Update()
    {
        heado.GetComponent<Image>().sprite = heads[x];
        clotho.GetComponent<Image>().sprite = clothes[y];
        shoeo.GetComponent<Image>().sprite = shoes[z];
        //_animator.SetInteger("Headgear",x);
        //_animator2.SetInteger("Clothgear",y);
       //_animator3.SetInteger("Shoegear",z);
        if(saving)
        {
            character.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Keypad0) || arbitrarynum == 0)
            {
                SaveLoad.Save(0);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || arbitrarynum == 1)
            {
                SaveLoad.Save(1);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || arbitrarynum == 2)
            {
                SaveLoad.Save(2);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || arbitrarynum == 3)
            {
                SaveLoad.Save(3);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || arbitrarynum == 4)
            {
                SaveLoad.Save(4);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || arbitrarynum == 5)
            {
                SaveLoad.Save(5);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || arbitrarynum == 6)
            {
                SaveLoad.Save(6);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || arbitrarynum == 7)
            {
                SaveLoad.Save(7);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || arbitrarynum == 8)
            {
                SaveLoad.Save(8);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || arbitrarynum == 9)
            {
                SaveLoad.Save(9);
                savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
        }

        //if(clothes[y].name == "Prototype_CharacterFront")
        //{
        //    _animator2.SetInteger("direction", 1);
        //    _animator2.moving = true;
        //}
    }

    public void head(int a)
    {
        if ((x + a >= 0) && (x + a < heads.Count))
        {
            x += a;
        }
        else if(x+a < 0)
        {
            x = heads.Count - 1;
        }
        else
        {
            x = 0;
        }
        text1.text = heads[x].name;
        headc = heads[x].name;
    }

    public void clothe(int b)
    {
        if ((y+b >= 0) && (y+b < clothes.Count))
        {
            y += b;
        }
        else if(y+b < 0)
        {
            y = clothes.Count - 1;
        }
        else
        {
            y = 0;
        }
        text2.text = clothes[y].name;
        clothec = clothes[y].name;
    }

    public void shoe(int c)
    {
        if ((z + c >= 0) && (z + c < shoes.Count))
        {
            z += c;
        }
        else if(z+c < 0)
        {
            z = shoes.Count - 1;
        }
        else
        {
            z = 0;
        }
        text3.text = shoes[z].name;
        shoec = shoes[z].name;
    }

    public void save(bool open)
    {
        creation.SetActive(open);
        savemenu.SetActive(!open);
        saving = !open;
    }

    public void saveslot(int input)
    {
        arbitrarynum = input;
    }
}
