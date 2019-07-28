using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walkman;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    SongManager songManager;

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        //songManager.ChangeSpeed(1.5f);
        songManager.Play();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        songManager.Pause();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        songManager.Play();
    }

    public void TogglePause()
    {
        if(isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
        isPaused = !isPaused;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
