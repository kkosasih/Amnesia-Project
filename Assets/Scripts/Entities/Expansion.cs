using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expansion : MonoBehaviour {
    public bool isVertical;                 // Whether the expansion goes vertically
    public float speed;                     // Speed of expansion in units per second
    private SpriteRenderer _spriteRenderer; // The Sprite Renderer component attached

	// Use this for initialization
	void Start ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
}
