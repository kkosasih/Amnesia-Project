using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour {

    public Animator hanimator;

	void Start () {
        hanimator = this.GetComponent<Animator>();
	}
	
	void Update () {
		
	}
}
