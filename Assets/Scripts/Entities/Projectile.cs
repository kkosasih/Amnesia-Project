using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public Direction direction; // The direction that it travels in
    public float speed;         // How quickly it travels in units per second
    public float duration = 1;  // How long the projectile lasts before death

	// Use this for initialization
	void Start ()
    {
        // Rotate based on chosen direction
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
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Time.deltaTime * speed, 0, 0);
        duration -= Time.deltaTime;
        // Check for death
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
	}
}
