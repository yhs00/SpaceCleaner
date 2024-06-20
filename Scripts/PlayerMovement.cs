using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameObject player;
    public static int playerHealth;
    public static float moveSpeed = 5f;
    public static bool isPlayerMove;

    public static bool isPlayerRightRotate;
    public static bool isPlayerLeftRotate;

    Quaternion playerRotation;

    public GameObject dieEffect;
    public GameObject playerDestroySound;

    public Rigidbody rb;

    Vector3 movement;
    public Joystick joystick;

    Vector3 wallCheck;
    bool a;
    void Start()
    {
        player = this.gameObject;//플레이어 자신을 스태틱으로 바꾸기
        playerHealth = 1;
        playerRotation = Quaternion.Euler(new Vector3(0,this.gameObject.transform.rotation.eulerAngles.y,0)); //현재 플레이어의 회전값
        isPlayerMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") + joystick.Horizontal;//좌우이동(키보드입력 + 터치입력)
        movement.z = Input.GetAxisRaw("Vertical") + joystick.Vertical;//상하이동

        PlayerRotate();
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, Time.fixedDeltaTime); //현재의 nowRotation값만큼 회전

        Die();
    }

    void FixedUpdate()
    {
        if(isPlayerMove)
        {
            PlayerMove();
        }
    }

    void PlayerMove()
    {
        //이동
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = Vector3.zero;//이거해야 부딪혔을때 안 튕겨나감

    }

    void PlayerRotate()//플레이어 회전 아이템을 먹을때 실행
    {
        if(isPlayerRightRotate)
        {
            playerRotation = playerRotation * Quaternion.Euler(new Vector3(0, 90, 0));
            isPlayerRightRotate = false;
            
        }

        if(isPlayerLeftRotate)
        {
            playerRotation = playerRotation * Quaternion.Euler(new Vector3(0, -90, 0));
            isPlayerLeftRotate = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "Item" || other.tag == "Laser")//적이나 아이템,레이저에 닿으면
        {
            other.GetComponent<BreakAtive>().health--;//닿은것체력과
            playerHealth--;//플레이어체력을 깎음
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "OutsideWall")//벽통과방지 코드1
        {
            wallCheck = collision.gameObject.transform.position - transform.position;//벽과 충돌하면 벽과 나의 위치를 계산
            if(a == false)
            {
                if (wallCheck.z > 0)//벽이 나보다 위에 있으면
                {
                    transform.Translate(new Vector3(0, 0, -0.05f) * Time.fixedDeltaTime * 10, Space.World);//아래로 살짝
                    Debug.Log("아래로");

                }
                else//벽이 나보다 아래에 있으면
                {
                    transform.Translate(new Vector3(0, 0, 0.05f) * Time.fixedDeltaTime * 10, Space.World);//위로 살짝
                    Debug.Log("위로");

                }
                a = true;
            }
            else
            {
                if (wallCheck.x > 0)//벽이 나보다 오른쪽에 있으면
                {
                    transform.Translate(new Vector3(-0.05f, 0, 0) * Time.fixedDeltaTime * 10, Space.World);//왼쪽으로 살짝
                    Debug.Log("왼쪽");


                }
                else//벽이 나보다 왼쪽에 있으면
                {
                    transform.Translate(new Vector3(0.05f, 0, 0) * Time.fixedDeltaTime * 10, Space.World);//오른쪽로 살짝
                    Debug.Log("오른쪽");
                }
                a = false;
            }

        }
    }


    public void Die()//사망시
    {
        if (playerHealth <= 0)
        {
            GameManager.isPlayerDie = true;//게임매니저에 플레이어 사망을 알려주기
            GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);//사망시 파괴효과
            Destroy(effect, 5f); 
            GameObject dieSound = Instantiate(playerDestroySound, transform.position, Quaternion.identity);//파괴사운드효과
            Destroy(dieSound, 5f);
            gameObject.SetActive(false);//파괴되니까 비활성화
        }
    }

}
