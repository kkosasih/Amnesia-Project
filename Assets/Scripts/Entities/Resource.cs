using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Resource : MonoBehaviour {
    #region Attributes
    protected Inventory inventory;  // The inventory of the object
    #endregion

    #region Event Functions
    protected virtual void Awake ()
    {
        inventory = GetComponent<Inventory>();
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion

    #region Methods

    #endregion
}
