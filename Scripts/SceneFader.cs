using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    public static SceneFader sceneFader;

    bool doubleClick = false; //��Ÿ�����ڵ�

    public void Start()
    {
        var a = FindObjectsOfType<SceneFader>();//�̰��ϴ� ���� �����ߴٰ� ���θ޴��� ���ư��� 2�� ����⶧��
        if (a.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            sceneFader = this;

            SceneManager.sceneLoaded += Loadedscene;//�� ��ȯ���� Loadedscene()����
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Loadedscene(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(int sceneNum)//1: �ٸ� ��ũ��Ʈ���� ������ �ϸ� �ش��ϴ� ���ڸ� �޾ƿ�
    {
        if(doubleClick)//�̰�������: �������� Ŭ���� �ϸ� �Է��� �������Ǹ鼭 �ڷ�ƾ�� �ߺ����� �����//����Ƚ����ŭ ���� �Ѿ
        {
            return;
        }
        StartCoroutine(FadeOut(sceneNum));//2: �װ� ���̵�ƿ��� �ִ´�
        doubleClick = true;
    }

    IEnumerator FadeIn()//�����Ҷ� ���̵���
    {
        
        for(float t = 1; t>0; t-=Time.deltaTime)//�������Ӹ��� ���
        {
            float a = curve.Evaluate(t);//�ش�Ǵ� �ð��� Ŀ�� ��ġ�� a�� �Ҵ�
            img.color = new Color(0f, 0f, 0f, a);//a�� ���� ��
            yield return 0;//������ ���� �ۿ��� ������ �� �����ӿ� ������ ����Ǿ ȿ���� ���� 
        }
    }

    IEnumerator FadeOut(int sceneNum)//3: ������ sceneNum�� �Ҵ�Ǵµ�
    {

        for (float t = 0; t < 1; t += Time.deltaTime)//�������Ӹ��� ��Ӱ�
        {
            float a = curve.Evaluate(t);//�ش�Ǵ� �ð��� Ŀ�� ��ġ�� a�� �Ҵ�
            img.color = new Color(0f, 0f, 0f, a);//a�� ���� ��
            yield return 0;//������ ���� �ۿ��� ������ �� �����ӿ� ������ ����Ǿ ȿ���� ���� 
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneNum);//4: ���̵�ƿ��� �ϰ� �� ���ڿ� ���� �ε��Ų��
        // 0.�����, 1.������������, -SceneManager.GetActiveScene().buildIndex. ���θ޴���(���� ����ѹ���ŭ ���ϱ� �׻� 0�� ���´�)
        //���� sceneFader.FadeTo(1); �̷��� ���� ���������� �ε��
        doubleClick = false;
    }


}
