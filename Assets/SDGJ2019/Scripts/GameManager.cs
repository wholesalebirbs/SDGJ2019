using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Walkman;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    SongManager songManager;

    public GameObject pausedPanel;
    public GameObject failPanel;

    private bool isPaused;

    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //songManager.ChangeSpeed(1.5f);
        songManager.Play();
        pausedPanel.SetActive(false);
        failPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        songManager.Pause();
        pausedPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        songManager.Play();
        pausedPanel.SetActive(false);
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerDeath()
    {
        failPanel.SetActive(true);
        Time.timeScale = 0;
        songManager.Pause();
    }
}
