using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSpeedManager : MonoBehaviour
{
    public Slider speedSlider;

    // Start is called before the first frame update
    void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("MoveSpeed",5);//�����ߴ� �ӵ����� �ҷ��ͼ� �����̴����� �ֱ� �ʱⰪ�� 5
        //���� moveSpeed�� ���� �ȹٲ㵵 �Ǵ� ������ �˾Ƽ� MoveSpeedOption()�� ���� �ٲ� �ѹ� ����Ǳ� ������
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveSpeedOption()//�÷��̾� �̵��ӵ� ����
    {
        float speed = speedSlider.value;
        Debug.Log(speed);
        PlayerMovement.moveSpeed = speed;
        PlayerPrefs.SetFloat("MoveSpeed", speed);
    }
}
