﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour {
    public int type;
    public Vector2 position;
    public bool hasAttack = false;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        UpdatePosition(position);
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Change position of the tile
    public void UpdatePosition (Vector2 positionP)
    {
        position = positionP;
        transform.localPosition = position;
    }

    // Switch the tile's attack state
    public IEnumerator GiveAttack (float time)
    {
        hasAttack = true;
        _spriteRenderer.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(time);
        hasAttack = false;
        UpdateColor();
    }

    // Update the color of the tile
    private void UpdateColor ()
    {
        switch (type)
        {
            case 0:
                _spriteRenderer.color = new Color(0, 1, 0);
                break;
            case 1:
                _spriteRenderer.color = new Color(1, 1, 0);
                break;
            case 2:
                _spriteRenderer.color = new Color(0, 0, 1);
                break;
        }
    }
}
