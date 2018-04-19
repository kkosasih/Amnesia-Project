using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLooks : MonoBehaviour {
    #region Attributes
    //public Animator _hair, _head, _clothes, _pants;
    public Animator pHair, pHead, pClothes, pPants;
    public List<RuntimeAnimatorController> lHair, lHead, lClothes, lPants;
    private int x = 0, y = 0, z = 0, h = 0, animanum = 0;
    #endregion

    #region Properties
    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
    }

    public int H
    {
        get
        {
            return h;
        }
    }
    #endregion

    #region Event Functions
    void Awake ()
    {
        Set();
        anima(0);
    }

    void Start ()
    {

    }

    void Update ()
    {  
        
    }
    #endregion

    #region Methods
    public void Set ()
    {
        //_head.runtimeAnimatorController = lHead[x];
        pHead.runtimeAnimatorController = lHead[x];
        //_clothes.runtimeAnimatorController = lClothes[y];
        pClothes.runtimeAnimatorController = lClothes[y];
        //_pants.runtimeAnimatorController = lPants[z];
        pPants.runtimeAnimatorController = lPants[z];
        //_hair.runtimeAnimatorController = lHair[h];
        pHair.runtimeAnimatorController = lHair[h];
    }

    public void head(int input)
    {
        x = Mathf.Clamp((x + input), 0, lHead.Count - 1);
        //_head.runtimeAnimatorController = lHead[x];
        pHead.runtimeAnimatorController = lHead[x];
    }

    public void clothe(int input)
    {
        y = Mathf.Clamp((y + input), 0, lClothes.Count - 1);
        //_clothes.runtimeAnimatorController = lClothes[y];
        pClothes.runtimeAnimatorController = lClothes[y];
    }

    public void shoe(int input)
    {
        z = Mathf.Clamp((z + input), 0, lPants.Count - 1);
        //_pants.runtimeAnimatorController = lPants[z];
        pPants.runtimeAnimatorController = lPants[z];
    }

    public void hair(int input)
    {
        h = Mathf.Clamp((h + input), 0, lHair.Count - 1);
        //_hair.runtimeAnimatorController = lHair[h];
        pHair.runtimeAnimatorController = lHair[h];
    }

    public void anima(int increment)
    {
        animanum = (animanum + increment) % 4;
        //_hair.SetInteger("direction", animanum);
        //_head.SetInteger("direction", animanum);
        //_clothes.SetInteger("direction", animanum);
        //_pants.SetInteger("direction", animanum);    
    }
    #endregion
}
