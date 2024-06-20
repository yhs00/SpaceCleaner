using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    public GameObject whiteHole;
    public GameObject blackHoleSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Untagged")//플레이어 콜라이더에 닿으면
        {
            //PlayerMovement.player.transform.rotation = whiteHole.transform.rotation;
            PlayerMovement.player.transform.position = whiteHole.transform.position; // 플레이어 이동 이렇게하는 이유 플레이어와 플레이어 콜라이더가 서로 다른 자리에 있어서
            GameObject bSound = Instantiate(blackHoleSound, transform.position, Quaternion.identity);//블랙홀 사용 효과
            Destroy(bSound, 5f);
        }
        else if(other.tag == "DeathWall" || other.tag == "OutsideWall")//버그처리
        {
            //바깥벽들은 회전할때 부딪치게 되는데 움직이면 안되니까
        }
        else
        {
            other.gameObject.transform.position = whiteHole.transform.position;

            GameObject bSound = Instantiate(blackHoleSound, transform.position, Quaternion.identity);//블랙홀 사용 효과
            Destroy(bSound, 5f);
        }
    }

}
