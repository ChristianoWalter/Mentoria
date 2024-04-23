using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject gameOverPanel;

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

    public void GameOver()
    {
        StartCoroutine(GameOverSquence());
    }
}
