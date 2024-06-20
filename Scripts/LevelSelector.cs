
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public List<GameObject> stageList;
    public List<Button> buttons;
    public List<Image> starImages;

    public void Start()
    {

        Debug.Log(PlayerPrefs.GetInt("levelReached"));
        buttons = new List<Button>();
        for(int i = 0;i < stageList.Count; i++)//�������� ��ư���� ��ư ����Ʈ�� �ڵ����� �ִ� �Լ�
        {
            buttons.AddRange(stageList[i].GetComponentsInChildren<Button>());//������������Ʈ�� �ڽ����� �ִ� ��ư�� ��ư����Ʈ�� �ְ� ������������Ʈ ����ŭ �ݺ��ϱ�
        }//�̷��� �������� ��� ����� ���鶧 ����


        for(int i = 0; i < buttons.Count; i++)//�������� ��� �Լ�
        {
            if(i > PlayerPrefs.GetInt("levelReached"))//�� ������������ �ڿ��ִ� ��ư�� 
            {
                buttons[i].interactable = false;//��Ȱ��ȭ
            }   
        }

        for (int i = 0; i < PlayerPrefs.GetInt("levelReached"); i++)//�������� �� �޼� �Լ�
        {
            starImages = new List<Image>(); //����Ʈ �ʱ�ȭ
            starImages.AddRange(buttons[i].GetComponentsInChildren<Image>()); //0.LevelButton 1.Group 2.Star 3.Star1 4.Star2 �̹��� ��ü�� �ҷ��ͼ� 0,1�ڸ��� ������
            for (int a = 0; a < PlayerPrefs.GetInt("Stage" + i);  a++)//PlayerPrefs.GetInt("Stage" + i)�� 1,2,3�߿� �ϳ��� ����
            {
                starImages[a + 2].GetComponent<Image>().color = new Color(0.4f, 1, 0, 1);//+2�ϴ� ���� ���� ���������� ����Ʈ�� 3��°���� Star�̹����� �����⶧��
            }
            Debug.Log(PlayerPrefs.GetInt("Stage" + i));
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void Select(int level)//������ư
    {
        SceneFader.sceneFader.FadeTo(level);
    }

    public void Back()//�ڷΰ���
    {
        SceneFader.sceneFader.FadeTo(-1);
    }

    public void StageSelect(int stage)//����������ư
    {
        for(int i = 0;i < stageList.Count; i++)
        {
            stageList[i].SetActive(false);
        }
        stageList[stage].SetActive(true);
    }

}
