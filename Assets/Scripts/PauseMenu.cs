using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    bool Paused;
    [SerializeField] GameObject menu;

    public const string PAUSE = "Pause";

    public void Start()
    {
        Paused = false;
    }

    public void Update()
    {
        if (Input.GetButtonDown(PAUSE))
        {
            TogglePause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Paused = true;
        menu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        Paused = false;
        menu.SetActive(false);
    }

    public void TogglePause()
    {
        switch (Paused)
        {
            case true:
                Unpause();
                break;
            case false:
                Pause();
                break;
        }
    }

    public bool isPaused()
    {
        return Paused;
    }
}
