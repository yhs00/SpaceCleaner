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
        masterSlider.value = PlayerPrefs.GetFloat("masterSound");//�����ߴ� �������� �ҷ��ͼ� �����̴����� �ֱ�
        //���� mastermixer�� ���� �ȹٲ㵵 �Ǵ� ������ �˾Ƽ� MasterSlide()�� ���� �ٲ� �ѹ� ����Ǳ� ������

        if (PlayerPrefs.GetFloat("mute",1) == 0)//���� ��������(����ȯ����) ���ҰŸ� �ߴٸ� ���� ,1)�ϴ�����: ���� ó��ų�� �ʱⰪ�� 1�� �ƴϸ� ���Ұ� ���·� ����Ǿ
        {
            muteButton.SetActive(false);
            unMuteButton.SetActive(true);
            AudioListener.volume = 0;//���Ұŷ� �����
        }
        
    }

    public void MasterSlide()//��������
    {
        float sound = masterSlider.value;
        mastermixer.SetFloat("Master", sound);
        PlayerPrefs.SetFloat("masterSound", sound);
    }

    public void MuteButton(float mute)//���ҰŹ�ư���� �����޾ƿ� 0�� ���Ұ� ����, 1�� ���Ұ� ����
    {
        PlayerPrefs.SetFloat("mute", mute);
        AudioListener.volume = mute;
    }
}
