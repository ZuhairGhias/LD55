using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.SetValueWithoutNotify(AudioManager.GetCurrentVolume());
    }

    public void StartGame()
    {
        GameManager.StartGame();
    }

    public void AdjustVolume(float volume)
    {
        AudioManager.SetVolume(volume);
    }


}
