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
        if (gameEnded)//���� ������ �ݺ�����
        {
            return;
        }

        if (allEnemyCount == 0 && isPlayerDie == false) //���� ��� ���� �ǰ� �÷��̾ ����־�� Ŭ����
        {
            
            StartCoroutine(GameClearMenu()); //Ŭ����
            gameEnded = true;
        }


        if(isPlayerDie) //�÷��̾� ����� ���ӿ���
        {            
            gameOverInfoText.text = "Player death...";
            StartCoroutine(GameOverMenu());
            gameEnded = true;
        }

        if (isSomethingOut) //�߻��� ���𰡰� �ʹ��� ������ ���ӿ���
        {
            gameOverInfoText.text = "Something has escaped...";
            StartCoroutine(GameOverMenu());
            gameEnded = true;
        }

    }

    IEnumerator GameOverMenu() //UI����
    {
        yield return new WaitForSeconds(2f);
        AudioList.isBgmPlay = false;//�й������� ��� ����
        AudioList.isGameOverPlay = true;//���ӿ��� �Ҹ��ѱ�
        gameOverUi.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator GameClearMenu() //UI����
    {
        PlayerMovement.isPlayerMove= false;
        PlayerLaser.isPlayerShoot = false;
        yield return new WaitForSeconds(2f);
        AudioList.isBgmPlay = false;//�¸������� ��� ����
        AudioList.isWinPlay = true;//�¸� �Ҹ� �ѱ�
        gameClearUi.SetActive(true);//Ŭ���� UI����
        Time.timeScale = 0f;

        //���� �ִ��� ������������ ���� ���������� Ŭ���� ������ levelReached�� �۾����� ���� ���� if��
        //���� levelReached��(�ִ��� ����������) ���� Ŭ������ ������������ ������ ����
        if (PlayerPrefs.GetInt("levelReached") < SceneManager.GetActiveScene().buildIndex - 2 + 1)
        {
            PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex - 2 + 1);
            //���� �����Ϳ��� �������� ��������Ұǵ� ���� Ȱ��ȭ�� ���� �����ε����� Ȯ���� �� ���ڸ�ŭ ������
            //�����ε����� ����ȭ��� �������� ����Ʈ ���� �����ϱ� -2 Ŭ���� �����ϱ� +1 (����: 1-1�� Ŭ���� �ϸ� ��������ε��� 2���� -2+1 �ϱ� levelReached���� 1�� �Էµ�) 
            //LevelSelector.buttons �迭 0��°�� 1-1�������� 1��°�� 1-2��������
        }
        Debug.Log(PlayerPrefs.GetInt("levelReached"));
        
        if(allBatteryCount == 0 && allBAWallCount == 0)//���͸��� �μ����º��� �� ó���ϸ�(ó������ ����� 0>=0 �� �Ǵϱ� ����ó�����ص���)
        {
            Debug.Log("��3��");
            ClearBattery(3);
        }
        else if(allBatteryCount == 0 || allBAWallCount == 0)//�� �� �ϳ��� ó���ϸ�
        {
            Debug.Log("��2��");
            ClearBattery(2);
        }
        else//�Ѵ� ���ϸ�
        {
            Debug.Log("��1��");
            ClearBattery(1);
        }

    }

    public void ClearBattery(int star)
    {
        if (PlayerPrefs.GetInt("Stage" + (SceneManager.GetActiveScene().buildIndex - 2)) < star)//star��Ŭ ������ ������ ����
        {
            PlayerPrefs.SetInt("Stage" + (SceneManager.GetActiveScene().buildIndex - 2), star);
        }
        //SceneManager.GetActiveScene().buildIndex - 2 == LevelSelector.buttons�� �ش��ϴ� �������� ��ġ�� ����

        for(int i = 0; i < PlayerPrefs.GetInt("Stage" + (SceneManager.GetActiveScene().buildIndex - 2)); i++)//Ŭ���� ȭ�� �� ǥ��
        {
            stars[i].GetComponent<Image>().color = new Color(0.4f, 1, 0, 1);
        }
    }
}
