using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [Range(0f, 1f)]
    public float DefaultVolume;

    private const string VolumePrefKey = "Volume";

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
        audioSource = GetComponent<AudioSource>();
        DebugUtils.HandleErrorIfNullGetComponent(audioSource, this);

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
        Instance.audioSource.volume = volume;
        print(volume);
    }

    
}
