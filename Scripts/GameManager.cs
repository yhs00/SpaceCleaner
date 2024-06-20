using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameEnded = false;

    public static int allEnemyCount;
    public static int allBatteryCount;
    public static int allBAWallCount;//BA==BreakAble
    public static bool isPlayerDie;
    public static bool isSomethingOut;

    public GameObject gameOverUi;
    public TextMeshProUGUI gameOverInfoText;

    public GameObject gameClearUi;
    public GameObject[] stars;

    public void Awake()
    {
        allEnemyCount = 0;
        allBatteryCount = 0;
        allBAWallCount= 0;
    }
    public void Start()
    {
        isPlayerDie = false;
        isSomethingOut = false;

    }

    public void Update()
    {
        if (gameEnded)//게임 끝나면 반복안함
        {
            return;
        }

        if (allEnemyCount == 0 && isPlayerDie == false) //적이 모두 제거 되고 플레이어가 살아있어야 클리어
        {
            
            StartCoroutine(GameClearMenu()); //클리어
            gameEnded = true;
        }


        if(isPlayerDie) //플레이어 사망시 게임오버
        {            
            gameOverInfoText.text = "Player death...";
            StartCoroutine(GameOverMenu());
            gameEnded = true;
        }

        if (isSomethingOut) //발사한 무언가가 맵밖을 나가면 게임오버
        {
            gameOverInfoText.text = "Something has escaped...";
            StartCoroutine(GameOverMenu());
            gameEnded = true;
        }

    }

    IEnumerator GameOverMenu() //UI띄우기
    {
        yield return new WaitForSeconds(2f);
        AudioList.isBgmPlay = false;//패배했으니 브금 끄기
        AudioList.isGameOverPlay = true;//게임오버 소리켜기
        gameOverUi.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator GameClearMenu() //UI띄우기
    {
        PlayerMovement.isPlayerMove= false;
        PlayerLaser.isPlayerShoot = false;
        yield return new WaitForSeconds(2f);
        AudioList.isBgmPlay = false;//승리했으니 브금 끄기
        AudioList.isWinPlay = true;//승리 소리 켜기
        gameClearUi.SetActive(true);//클리어 UI띄우기
        Time.timeScale = 0f;

        //원래 최대기록 스테이지보다 이전 스테이지를 클리어 했을때 levelReached가 작아지는 버그 방지 if문
        //만약 levelReached가(최대기록 스테이지가) 현재 클리어한 스테이지보다 작으면 실행
        if (PlayerPrefs.GetInt("levelReached") < SceneManager.GetActiveScene().buildIndex - 2 + 1)
        {
            PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex - 2 + 1);
            //레벨 셀렉터에서 스테이지 잠금해제할건데 현재 활성화된 씬의 빌드인덱스를 확인해 그 숫자만큼 열거임
            //빌드인덱스에 메인화면과 스테이지 셀렉트 씬이 있으니까 -2 클리어 했으니까 +1 (예시: 1-1을 클리어 하면 현재빌드인덱스 2에서 -2+1 니까 levelReached에는 1이 입력됨) 
            //LevelSelector.buttons 배열 0번째가 1-1스테이지 1번째가 1-2스테이지
        }
        Debug.Log(PlayerPrefs.GetInt("levelReached"));
        
        if(allBatteryCount == 0 && allBAWallCount == 0)//배터리와 부서지는벽을 다 처리하면(처음부터 없었어도 0>=0 이 되니까 예외처리안해도됨)
        {
            Debug.Log("별3개");
            ClearBattery(3);
        }
        else if(allBatteryCount == 0 || allBAWallCount == 0)//둘 중 하나만 처리하면
        {
            Debug.Log("별2개");
            ClearBattery(2);
        }
        else//둘다 안하면
        {
            Debug.Log("별1개");
            ClearBattery(1);
        }

    }

    public void ClearBattery(int star)
    {
        if (PlayerPrefs.GetInt("Stage" + (SceneManager.GetActiveScene().buildIndex - 2)) < star)//star성클 한적이 없으면 실행
        {
            PlayerPrefs.SetInt("Stage" + (SceneManager.GetActiveScene().buildIndex - 2), star);
        }
        //SceneManager.GetActiveScene().buildIndex - 2 == LevelSelector.buttons의 해당하는 스테이지 위치와 같다

        for(int i = 0; i < PlayerPrefs.GetInt("Stage" + (SceneManager.GetActiveScene().buildIndex - 2)); i++)//클리어 화면 별 표시
        {
            stars[i].GetComponent<Image>().color = new Color(0.4f, 1, 0, 1);
        }
    }
}
