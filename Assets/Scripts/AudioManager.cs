using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float DefaultVolume;
    public AudioClip[] SoundEffects;
    public AudioClip[] MusicTracks;
    public AudioSource Jukebox;
    public AudioSource FXplayer;

    public const string VolumePrefKey = "Volume";

    private static AudioManager Instance;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if(!PlayerPrefs.HasKey(VolumePrefKey))
        {
            PlayerPrefs.SetFloat(VolumePrefKey, DefaultVolume);
        }
    }

    public static float GetCurrentVolume()
    {
        return PlayerPrefs.GetFloat(VolumePrefKey);
    }

    public static void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        Instance.Jukebox.volume = volume;
        Instance.FXplayer.volume = volume;
        print(volume);
    }

    public static void PlaySoundEffect(string clipName)
    {
        PlaySoundEffect(clipName, 1f);
    }

    public static void PlaySoundEffect(string clipName, float volumeScale)
    {
        foreach (AudioClip clip in Instance.SoundEffects)
        {
            if (clip.name == clipName)
            {
                Instance.FXplayer.clip = clip;
                Instance.FXplayer.PlayOneShot(clip, volumeScale);
                return;
            }
        }
        Debug.Log("Sound Effect " + clipName + " not found in AudioManager!");
    }

    public static void PlaySoundEffect(AudioClip clip)
    {
        PlaySoundEffect(clip, 1f);
    }

    public static void PlaySoundEffect(AudioClip clip, float volumeScale)
    {
        Instance.FXplayer.clip = clip;
        Instance.FXplayer.PlayOneShot(clip, volumeScale);
    }

    public static void SetMusicTrack(string trackName)
    {
        foreach (AudioClip clip in Instance.MusicTracks)
        {
            if (clip.name == trackName)
            {
                Instance.Jukebox.clip = clip;
                return;
            }
        }
        Debug.Log("Track " + trackName + " not found in AudioManager!");
    }

    public static void PlayMusic(bool fade = false)
    {
        Instance.Jukebox.Play();

        if (fade)
        {
            Instance.Jukebox.volume = 0;
            Instance.Jukebox.DOFade(GetCurrentVolume(), 2);
        }
    }

    public static void PauseMusic(bool fade = false)
    {

        if (fade)
        {
            Instance.Jukebox.DOFade(0, 2);
        }
        else
        {
            Instance.Jukebox.Pause();
        }
    }
}
