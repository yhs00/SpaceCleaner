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
        speedSlider.value = PlayerPrefs.GetFloat("MoveSpeed",5);//저장했던 속도값을 불러와서 슬라이더값에 넣기 초기값은 5
        //따로 moveSpeed의 값을 안바꿔도 되는 이유는 알아서 MoveSpeedOption()가 값이 바뀔때 한번 실행되기 때문에
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveSpeedOption()//플레이어 이동속도 조절
    {
        float speed = speedSlider.value;
        Debug.Log(speed);
        PlayerMovement.moveSpeed = speed;
        PlayerPrefs.SetFloat("MoveSpeed", speed);
    }
}
