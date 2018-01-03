using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expansion : MonoBehaviour {
    public bool isVertical;
    public float speed;
    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
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
