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
        //PlayerPrefs.DeleteAll(); //�׽�Ʈ�� ������ �ʱ�ȭ
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//�ڷ�Ű�� ������ ����޴��� ������
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
