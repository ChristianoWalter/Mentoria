using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public bool isPaused;

    private void Awake()
    {
        instance = this;
    }
    public IEnumerator GameOverSquence()
    {
        yield return new WaitForSeconds(1f);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseAction(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            isPaused = true;
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverSquence());
    }
}
