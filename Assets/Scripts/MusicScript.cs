using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    private AudioSource _audioSource;
    private static MusicScript musicScript;

    private void Awake()
    {
        //this script is pretty much to make it so that
        //the music doesn't stop when you transition scenes
        //hence why it's on the sound manager object
        DontDestroyOnLoad(this.transform.gameObject);
        if(musicScript == null)
        {
            musicScript = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        //if music's already playing, do nothing
        //otherwise, play the song
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        //if you wanna stop the song and play something else, run this
        _audioSource.Stop();
    }
}
