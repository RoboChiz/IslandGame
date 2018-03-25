using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip backgroundMusic;

	// Use this for initialization
	void Start ()
    {
        FindObjectOfType<SoundManager>().PlayMusic(backgroundMusic);
	}
}
