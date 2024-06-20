using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool gameIsPaused;
    public GameObject pauseMenuUi;

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

    }

    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneFader.sceneFader.FadeTo(0); // == SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneFader.sceneFader.FadeTo(-SceneManager.GetActiveScene().buildIndex + 1); // == SceneManager.LoadScene(0 + 1); ���� �� ���ڸ�ŭ ���� 1���ϱ� �׷��� 1�� �Ǵϱ� ���������� ������
    }

    public void Next()
    {
        Time.timeScale = 1f;
        SceneFader.sceneFader.FadeTo(1); //== SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
