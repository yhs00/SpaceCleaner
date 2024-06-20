using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioList : MonoBehaviour
{
    public AudioSource mainBgm;
    public AudioSource stageBgm;
    public AudioSource winSound;
    public AudioSource gameOverSound;
    public AudioSource clickSound;

    public static bool isBgmPlay;
    public static bool isGameOverPlay;
    public static bool isWinPlay;

    public int preStageNum;

    // Start is called before the first frame update
    void Start()
    {
        var a = FindObjectsOfType<AudioList>();//이거하는 이유 실행했다가 메인메뉴로 돌아가면 2개 생기기때문
        if (a.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += Loadedscene;//씬 전환마다 Loadedscene()실행
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Loadedscene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.buildIndex);
        Debug.Log(preStageNum);
        isBgmPlay = true;
        isGameOverPlay = false;
        isWinPlay = false;

        if (scene.buildIndex == 0 || scene.buildIndex == 1)// 0 == 메인메뉴, 1 == 스테이지선택
        {
            if (preStageNum >= 2)//이전 씬이 스테이지였으면 스테이지 브금 끄고 메인브금 켜기
            {
                stageBgm.Stop();
                mainBgm.Play();
            }
  
        }
        else if(scene.buildIndex >= 2)// 2이상은 스테이지씬
        {
            mainBgm.Stop();
            stageBgm.Play();
        }

        preStageNum = scene.buildIndex;
    }

    void Update()
    {
        if (isBgmPlay == false && isWinPlay == true)//이겼으면 브금끄고 승리 사운드 실행
        {
            stageBgm.Stop();
            winSound.Play();
            isBgmPlay = true;//한번만 실행 할거니까
        }
        else if (isBgmPlay == false && isGameOverPlay == true)//졌으면 브금끄고 게임오버 사운드 실행
        {
            stageBgm.Stop();
            gameOverSound.Play();
            isBgmPlay = true;//한번만 실행 할거니까
        }

        if (Input.GetMouseButtonDown(0))//클릭시 소리
        {
            clickSound.Play();
        }


    }
}
