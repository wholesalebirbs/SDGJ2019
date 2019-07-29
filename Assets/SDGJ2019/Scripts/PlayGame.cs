using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void PlayGameNow()
    {
        SceneManager.LoadScene("GAME");
    }

    public void ExitGameNow()
    {
        Application.Quit();
    }
}
