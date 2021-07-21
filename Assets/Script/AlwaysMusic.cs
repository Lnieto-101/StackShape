using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysMusic : MonoBehaviour
{
	//Keep the music gameobjects on scene
    private AudioSource _audioSource;
    private void Awake()
    {
		if (!GameObject.Find("OriginalMusic"))
			DontDestroyOnLoad(transform.gameObject);
		else
			Destroy(this.gameObject);
		_audioSource = GetComponent<AudioSource>();

		gameObject.name = "OriginalMusic";
    }

     public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
 
    public void StopMusic()
    {
		_audioSource.Stop();
    }

	private void Update()
	{
		if (PlayerPrefs.GetInt("Music", 1) == 1)
			PlayMusic();
		else
			StopMusic();
	}
}
