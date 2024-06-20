using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemyRotate : MonoBehaviour
{
    public bool isRightRotate;
    public bool isLeftRotate;
    Quaternion nowRotation;
    Quaternion plusRightRotation;
    Quaternion plusLeftRotation;
    public GameObject playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        nowRotation = Quaternion.Euler(new Vector3(0, 0, 0)); //현재 회전값
        plusRightRotation = Quaternion.Euler(new Vector3(0, 90, 0));//우회전
        plusLeftRotation = Quaternion.Euler(new Vector3(0, -90, 0));//좌회전
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, nowRotation, Time.fixedDeltaTime); //현재의 nowRotation값만큼 회전

        if (isRightRotate == true)//만약 BreakAtive에서 true가 되면
        {
            StartCoroutine(PlayerColliderOff());
            nowRotation = nowRotation * plusRightRotation; // 현재 회전값에 우회전값을 더하기
            isRightRotate = false;
        }

        if (isLeftRotate == true)//만약 BreakAtive에서 true가 되면
        {
            StartCoroutine(PlayerColliderOff());
            nowRotation = nowRotation * plusLeftRotation; // 현재 회전값에 좌회전값을 더하기
            isLeftRotate = false;
        }
    }

    IEnumerator PlayerColliderOff()//이거하는이유:안하면 화면회전할때 플레이어랑 죄다 부딪힘
    {
        PlayerMovement.isPlayerMove = false;//회전하는 동안 플레이어의 움직임 제한
        PlayerLaser.isPlayerShoot = false;//회전하는 동안 플레이어의 발사 제한
        playerCollider.SetActive(false);
        yield return new WaitForSeconds(1f);
        PlayerMovement.isPlayerMove = true;//회전끝났으니 다시 움직이게
        PlayerLaser.isPlayerShoot = true;
        playerCollider.SetActive(true);
    }
}
