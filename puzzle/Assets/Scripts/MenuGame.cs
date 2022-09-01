using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void Reset()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
