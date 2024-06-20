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
        var a = FindObjectsOfType<AudioList>();//�̰��ϴ� ���� �����ߴٰ� ���θ޴��� ���ư��� 2�� ����⶧��
        if (a.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += Loadedscene;//�� ��ȯ���� Loadedscene()����
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

        if (scene.buildIndex == 0 || scene.buildIndex == 1)// 0 == ���θ޴�, 1 == ������������
        {
            if (preStageNum >= 2)//���� ���� �������������� �������� ��� ���� ���κ�� �ѱ�
            {
                stageBgm.Stop();
                mainBgm.Play();
            }
  
        }
        else if(scene.buildIndex >= 2)// 2�̻��� ����������
        {
            mainBgm.Stop();
            stageBgm.Play();
        }

        preStageNum = scene.buildIndex;
    }

    void Update()
    {
        if (isBgmPlay == false && isWinPlay == true)//�̰����� ��ݲ��� �¸� ���� ����
        {
            stageBgm.Stop();
            winSound.Play();
            isBgmPlay = true;//�ѹ��� ���� �ҰŴϱ�
        }
        else if (isBgmPlay == false && isGameOverPlay == true)//������ ��ݲ��� ���ӿ��� ���� ����
        {
            stageBgm.Stop();
            gameOverSound.Play();
            isBgmPlay = true;//�ѹ��� ���� �ҰŴϱ�
        }

        if (Input.GetMouseButtonDown(0))//Ŭ���� �Ҹ�
        {
            clickSound.Play();
        }


    }
}
