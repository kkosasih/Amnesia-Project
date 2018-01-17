using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOptions : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Destroy current objects and create new ones
    public void CreateOptions (List<string> options)
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        for (int i = 0; i < options.Count; ++i)
        {
            GameObject g = Instantiate(Resources.Load<GameObject>("GUI/ChoicePanel"), transform);
            RectTransform rt = g.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.2f, 0.35f + 0.1f * options.Count - 0.2f * i);
            rt.anchorMax = new Vector2(0.8f, 0.45f + 0.1f * options.Count - 0.2f * i);
            g.transform.Find("Text").GetComponent<Text>().text = options[i];
        }
    }
}
