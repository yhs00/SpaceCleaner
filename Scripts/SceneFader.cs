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

    bool doubleClick = false; //연타방지코드

    public void Start()
    {
        var a = FindObjectsOfType<SceneFader>();//이거하는 이유 실행했다가 메인메뉴로 돌아가면 2개 생기기때문
        if (a.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            sceneFader = this;

            SceneManager.sceneLoaded += Loadedscene;//씬 전환마다 Loadedscene()실행
            
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

    public void FadeTo(int sceneNum)//1: 다른 스크립트에서 실행을 하면 해당하는 숫자를 받아옴
    {
        if(doubleClick)//이거한이유: 연속으로 클릭을 하면 입력이 여러번되면서 코루틴이 중복으로 실행됨//누른횟수만큼 씬이 넘어감
        {
            return;
        }
        StartCoroutine(FadeOut(sceneNum));//2: 그걸 페이드아웃에 넣는다
        doubleClick = true;
    }

    IEnumerator FadeIn()//시작할때 페이드인
    {
        
        for(float t = 1; t>0; t-=Time.deltaTime)//매프레임마다 밝게
        {
            float a = curve.Evaluate(t);//해당되는 시간의 커브 수치를 a에 할당
            img.color = new Color(0f, 0f, 0f, a);//a는 알파 값
            yield return 0;//리턴을 포문 밖에다 넣으면 한 프레임에 여러번 실행되어서 효과가 없음 
        }
    }

    IEnumerator FadeOut(int sceneNum)//3: 넣으면 sceneNum에 할당되는데
    {

        for (float t = 0; t < 1; t += Time.deltaTime)//매프레임마다 어둡게
        {
            float a = curve.Evaluate(t);//해당되는 시간의 커브 수치를 a에 할당
            img.color = new Color(0f, 0f, 0f, a);//a는 알파 값
            yield return 0;//리턴을 포문 밖에다 넣으면 한 프레임에 여러번 실행되어서 효과가 없음 
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneNum);//4: 페이드아웃을 하고 그 숫자에 따라서 로드시킨다
        // 0.재시작, 1.다음스테이지, -SceneManager.GetActiveScene().buildIndex. 메인메뉴로(현재 빌드넘버만큼 빼니까 항상 0이 나온다)
        //예시 sceneFader.FadeTo(1); 이러면 다음 스테이지가 로드됨
        doubleClick = false;
    }


}
