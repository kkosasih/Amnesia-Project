using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public Direction direction;
    public float speed;
    public float duration = 1;

	// Use this for initialization
	void Start ()
    {
		switch(direction)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(0, Time.deltaTime * speed, 0);
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
	}
}
