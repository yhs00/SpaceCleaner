using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mastermixer;
    public Slider masterSlider;
    public GameObject muteButton;
    public GameObject unMuteButton;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterSound");//저장했던 음량값을 불러와서 슬라이더값에 넣기
        //따로 mastermixer의 값을 안바꿔도 되는 이유는 알아서 MasterSlide()가 값이 바뀔때 한번 실행되기 때문에

        if (PlayerPrefs.GetFloat("mute",1) == 0)//만약 끄기전에(씬전환전에) 음소거를 했다면 실행 ,1)하는이유: 게임 처음킬때 초기값이 1이 아니면 음소거 상태로 실행되어서
        {
            muteButton.SetActive(false);
            unMuteButton.SetActive(true);
            AudioListener.volume = 0;//음소거로 만들기
        }
        
    }

    public void MasterSlide()//볼륨조절
    {
        float sound = masterSlider.value;
        mastermixer.SetFloat("Master", sound);
        PlayerPrefs.SetFloat("masterSound", sound);
    }

    public void MuteButton(float mute)//음소거버튼에서 값을받아옴 0이 음소거 시작, 1이 음소거 중지
    {
        PlayerPrefs.SetFloat("mute", mute);
        AudioListener.volume = mute;
    }
}
