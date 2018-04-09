using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expansion : MonoBehaviour {
    #region Attributes
    public bool isVertical;                 // Whether the expansion goes vertically
    public float speed;                     // Speed of expansion in units per second
    private SpriteRenderer _spriteRenderer; // The Sprite Renderer component attached
    #endregion

    #region Event Functions
    void Awake ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Control expansion to be vertical or horizontal
        if (isVertical)
        {
            _spriteRenderer.size += new Vector2(0, Time.deltaTime * speed);
        }
        else
        {
            _spriteRenderer.size += new Vector2(Time.deltaTime * speed, 0);
        }
    }
    #endregion
}
