
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
        for(int i = 0;i < stageList.Count; i++)//스테이지 버튼들을 버튼 리스트에 자동으로 넣는 함수
        {
            buttons.AddRange(stageList[i].GetComponentsInChildren<Button>());//스테이지리스트의 자식으로 있는 버튼을 버튼리스트에 넣고 스테이지리스트 수만큼 반복하기
        }//이러면 스테이지 잠금 기능을 만들때 편함


        for(int i = 0; i < buttons.Count; i++)//스테이지 잠금 함수
        {
            if(i > PlayerPrefs.GetInt("levelReached"))//깬 스테이지보다 뒤에있는 버튼은 
            {
                buttons[i].interactable = false;//비활성화
            }   
        }

        for (int i = 0; i < PlayerPrefs.GetInt("levelReached"); i++)//스테이지 별 달성 함수
        {
            starImages = new List<Image>(); //리스트 초기화
            starImages.AddRange(buttons[i].GetComponentsInChildren<Image>()); //0.LevelButton 1.Group 2.Star 3.Star1 4.Star2 이미지 전체를 불러와서 0,1자리는 사용안함
            for (int a = 0; a < PlayerPrefs.GetInt("Stage" + i);  a++)//PlayerPrefs.GetInt("Stage" + i)는 1,2,3중에 하나만 나옴
            {
                starImages[a + 2].GetComponent<Image>().color = new Color(0.4f, 1, 0, 1);//+2하는 이유 위와 같은이유로 리스트에 3번째부터 Star이미지가 나오기때문
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

    public void Select(int level)//레벨버튼
    {
        SceneFader.sceneFader.FadeTo(level);
    }

    public void Back()//뒤로가기
    {
        SceneFader.sceneFader.FadeTo(-1);
    }

    public void StageSelect(int stage)//스테이지버튼
    {
        for(int i = 0;i < stageList.Count; i++)
        {
            stageList[i].SetActive(false);
        }
        stageList[stage].SetActive(true);
    }

}
