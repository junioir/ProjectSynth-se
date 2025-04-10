using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _Playlist;
    [SerializeField] private AudioSource _Audiosource;
    private int _MusicIndex;
    void Start()
    {
        _Audiosource = GetComponent<AudioSource>();
        _Audiosource.clip = _Playlist[0];
        _Audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_Audiosource.isPlaying)
        {
            PlayNextSong();
        }

    }

    public void PlayNextSong()
    {
      _MusicIndex = (_MusicIndex + 1) % _Playlist.Length;
        _Audiosource.clip = _Playlist[_MusicIndex];
        _Audiosource.Play();
    }

}