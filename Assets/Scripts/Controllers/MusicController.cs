using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    #region Attributes
    public static MusicController instance; // The instance to reference
    private AudioSource _audioSource;       // The audio source component attached
    #endregion

    #region Event Functions
    void Awake ()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
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

    #region Coroutines
    // Smoothly change the audio file
    public IEnumerator ChangeMusic (AudioClip newClip)
    {
        if (_audioSource.clip == newClip)
        {
            yield break;
        }
        float oldVolume = _audioSource.volume;
        for (float f = 0f; f < 1f; f += Time.deltaTime)
        {
            _audioSource.volume = oldVolume * (1f - f);
            yield return null;
        }
        _audioSource.Stop();
        _audioSource.clip = newClip;
        _audioSource.volume = oldVolume;
        _audioSource.Play();
    }
    #endregion
}
