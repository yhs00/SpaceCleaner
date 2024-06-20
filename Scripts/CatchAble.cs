using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CatchAble : MonoBehaviour
{
    public bool isPlayerEnter;
    public bool isFireReady;
    public bool isRightItem;
    public bool isLeftItem;

    public bool isPlayerRightItem;
    public bool isPlayerLeftItem;

    public bool isBattery;

    public AllEnemyRotate allEnemyRotate;

    public Vector3 playerLook;

    public GameObject catchSound;

    private void Awake()
    {
        isPlayerEnter = false;
        isFireReady = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        allEnemyRotate = transform.parent.gameObject.GetComponent<AllEnemyRotate>(); //시작할때 AllEnemyRotate의 스크립트 참조

        if (isBattery)//배터리 갯수 확인하기
        {
            GameManager.allBatteryCount++;//배터리라면 게임매니저에 있는 카운트 1 올리기 그래서 전체 배터리 갯수 확인
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerEnter == true)//플레이어에서 트루가 되면 자식오브젝트로 들어가기
        {
            if(!isFireReady) //한번만 실행
            {
                Sound();
                gameObject.GetComponent<Collider>().enabled = false;//잡은 아이템이 발사전에 충돌로 파괴되지 않게 콜라이더 비활성화
                transform.SetParent(PlayerMovement.player.transform);//플레이어를 부모로
                transform.localScale = Vector3.one * 0.5f;
                isFireReady = true;//발사가능

            }
            else //반복 실행
            {
                transform.localPosition = Vector3.forward * PlayerLaser.laserDistance;//발사위치는 플레이어 앞으로
                transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime * 10);//잡았다는 표시로 아이템 회전
            }

            if (this.gameObject.tag == "Item")//만약 잡힌게 아이템이라면
            {
                Item();
            }


        }

        if (!isPlayerEnter&& isFireReady)//플레이어에서 false가 되면 실행// 조건 없으면 게임시작부터 발사되기때문 
        {

            if(transform.parent != null)//한번만 실행
            {
                playerLook = PlayerMovement.player.transform.forward; //플레이어의 방향 확인
                transform.parent = null;
            }//이거 하는 이유: 안하면 발사중 플레이어가 회전했을때 발사된 오브젝트도 같이 회전함

            gameObject.GetComponent<Collider>().enabled = true;//잡은 아이템이 발사후 충돌로 파괴되게 콜라이더 활성화
            gameObject.GetComponent<BreakAtive>().triggerAtive = true;//BreakAtive.triggerAtive 활성화
            transform.Translate(playerLook * Time.deltaTime * 10, Space.World);// 플레이어 기준으로 앞으로 발사
            
        }

    }

    void Item()
    {
        if (isRightItem)//잡은 물체가 우회전아이템이면
        {
            allEnemyRotate.isRightRotate = true; // allEnemyRotate함수에서 isRightRotate를 true로 바꾸기
            Debug.Log("우회전 획득!");
        }


        if (isLeftItem)//잡은 물체가 좌회전아이템이면
        {
            allEnemyRotate.isLeftRotate = true; // allEnemyRotate함수에서 isLeftRotate를 true로 바꾸기
            Debug.Log("좌회전 획득!");
        }

        if(isBattery)
        {
            GameManager.allBatteryCount--;//잡았으니까 카운트 1빼기
            Debug.Log("배터리 획득!");
        }


        if(isPlayerRightItem)
        {
            PlayerMovement.isPlayerRightRotate = true;
            Debug.Log("플레이어 우회전!");
        }

        if (isPlayerLeftItem)
        {
            PlayerMovement.isPlayerLeftRotate = true;
            Debug.Log("플레이어 좌회전!");
        }

        Sound();
        gameObject.SetActive(false);
    }

    void Sound()
    {
        GameObject cSound = Instantiate(catchSound, transform.position, Quaternion.identity);//잡은사운드효과 적,아이템,배터리 다르게 넣으면 됨
        Destroy(cSound, 5f);
    }
}
