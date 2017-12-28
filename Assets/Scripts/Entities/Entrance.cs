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
    public IEnumerator TeleportPlayer ()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerCharacter>().enabled = false;
        SceneManager.LoadScene(sceneTo);
        for (string cp = GameController.map.path; cp == GameController.map.path;)
        {
            yield return new WaitForEndOfFrame();
        }
        player.GetComponent<PlayerCharacter>().enabled = true;
        player.GetComponent<PlayerCharacter>().currentTile = tileFrom;
        player.transform.position = GameController.map.tiles[tileFrom].transform.position;
        player.GetComponent<PlayerCharacter>().Move(tileTo);
    }
}
