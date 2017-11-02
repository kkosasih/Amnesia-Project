using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUnique : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
