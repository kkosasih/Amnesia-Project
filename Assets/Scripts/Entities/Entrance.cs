using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour {
    public int sceneTo;
    public int tileFrom;
    public int tileTo;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    // Teleport the player to the other side of the entrance
    public void TeleportPlayer ()
    {
        SceneManager.LoadScene(sceneTo);
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerCharacter>().currentTile = tileFrom;
        player.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<GameController>().map.tiles[tileFrom].transform.position;
        player.GetComponent<PlayerCharacter>().Move(tileTo);
    }
}
