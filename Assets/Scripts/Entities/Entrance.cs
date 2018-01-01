using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        GameObject player = GameObject.FindWithTag("Player");
        Image mask = GameObject.FindWithTag("UIMask").GetComponent<Image>();
        player.GetComponent<PlayerCharacter>().onMap = false;
        yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 1), 0.5f));
        player.GetComponent<PlayerCharacter>().currentTile = tileFrom;
        yield return StartCoroutine(GameController.SetUpScene(sceneTo));
        if (!DialogueTracking.CheckConversation())
        {
            yield return StartCoroutine(Helper.ChangeColorInTime(mask, new Color(0, 0, 0, 0), 0.5f));
            player.GetComponent<PlayerCharacter>().Move(tileTo);
        }
        Destroy(gameObject);
    }
}
