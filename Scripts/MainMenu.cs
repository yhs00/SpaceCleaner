using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool gameQuit;
    public GameObject quitMenuUI;

    private void Start()
    {
        //PlayerPrefs.DeleteAll(); //테스트용 데이터 초기화
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//뒤로키를 눌러도 종료메뉴가 나오게
        {
            if (!gameQuit)
            {
                quitMenuUI.SetActive(true);
                gameQuit = true;
            }
            else
            {
                quitMenuUI.SetActive(false);
                gameQuit = false;
            }
        }

    }

    public void PlayGame()
    {
        SceneFader.sceneFader.FadeTo(1); // == SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
