using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLooks : MonoBehaviour {
    #region Attributes
    public GameObject savemenu, ccmenu, creation;
    public Animator _hair, _head,_clothes, _pants, _Background;
    public int x = 0, y = 0, z = 0,h = 0, animanum = 0, arbitrarynum = 10;
    public bool saving = false;
    #endregion

    #region Event Functions
    void Start()
    {
        _hair.SetInteger("Movement", 0);
        _hair.SetInteger("Image", 0);
        _head.SetInteger("Movement", 0);
        _head.SetInteger("Image", 0);
        _clothes.SetInteger("Movement", 0);
        _clothes.SetInteger("Image", 0);
        _pants.SetInteger("Movement", 0);
        _pants.SetInteger("Image", 0);
        _Background.SetBool("Saving",saving);
    }

    public void Update()
    {  
        if (saving)
        {
            //character.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Keypad0) || arbitrarynum == 0)
            {
                SaveLoad.Save(0);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || arbitrarynum == 1)
            {
                SaveLoad.Save(1);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || arbitrarynum == 2)
            {
                SaveLoad.Save(2);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || arbitrarynum == 3)
            {
                SaveLoad.Save(3);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || arbitrarynum == 4)
            {
                SaveLoad.Save(4);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || arbitrarynum == 5)
            {
                SaveLoad.Save(5);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || arbitrarynum == 6)
            {
                SaveLoad.Save(6);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || arbitrarynum == 7)
            {
                SaveLoad.Save(7);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || arbitrarynum == 8)
            {
                SaveLoad.Save(8);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || arbitrarynum == 9)
            {
                SaveLoad.Save(9);
                //savemenu.SetActive(false);
                ccmenu.SetActive(false);
            }
        }
    }
    #endregion

    #region Methods
    public void head(int input)
    {
        if ((x + input >= 0) && (x + input < 3))
        {
            x += input;
        }
        else if (x + input < 0)
        {
            x = 2;
        }
        else
        {
            x = 0;
        }
        _head.SetInteger("Image", x);
    }

    public void clothe(int input)
    {
        if ((y + input >= 0) && (y + input < 3))
        {
            y += input;
        }
        else if (y + input < 0)
        {
            y = 2;
        }
        else
        {
            y = 0;
        }
        _clothes.SetInteger("Image", y);
    }

    public void shoe(int input)
    {
        if ((z + input >= 0) && (z + input < 3))
        {
            z += input;
        }
        else if (z + input < 0)
        {
            z = 2;
        }
        else
        {
            z = 0;
        }
        _pants.SetInteger("Image", z);
    }

    public void hair(int input)
    {
        if ((h + input >= 0) && (z + input < 3))
        {
            h += input;
        }
        else if (h + input < 0)
        {
            h = 2;
        }
        else
        {
            h = 0;
        }
        _hair.SetInteger("Image", h);
    }

    public void anima(int increment)
    {
        if ((animanum + increment >= 0) && (animanum + increment < 4))
        {
            animanum += increment;
        }
        else if (animanum == 0)
        {
            animanum = 4 - 1;
        }
        else
        {
            animanum = 0;
        }

        _hair.SetInteger("Movement", animanum);
        _head.SetInteger("Movement", animanum);
        _clothes.SetInteger("Movement", animanum);
        _pants.SetInteger("Movement", animanum);    
    }

    public void save()
    {
        saving = !saving;
        creation.SetActive(!saving);
        _Background.SetBool("Saving", saving);
        savemenu.SetActive(saving);
    }

    public void saveslot(int input)
    {
        arbitrarynum = input;
    }
    #endregion
}
