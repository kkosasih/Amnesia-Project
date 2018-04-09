using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    #region Attributes
    public AudioClip music;
    private AudioSource _audioSource;
    #endregion

    #region Event Functions#
    void Awake ()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
    #endregion
}
