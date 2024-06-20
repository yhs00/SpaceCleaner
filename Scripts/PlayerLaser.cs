using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLaser : MonoBehaviour
{

    public LineRenderer lineRenderer;


    public bool isCatch;
    RaycastHit hit;
    public CatchAble hitObjScript;
    public AudioSource attackSound;
    public GameObject hitEffect;
    public static float laserDistance;

    public GameObject catchButton;
    public GameObject fireButton;

    public static bool isPlayerShoot;

    // Start is called before the first frame update
    void Start()
    {
        isCatch = false;
        isPlayerShoot = true;

        laserDistance = 1.8f;
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer component not found!");
                return;
            }
        }


        lineRenderer.positionCount = 2;//시작점,도착점2개필요하니까 2
    }

    // Update is called once per frame
    void Update()
    {
        

        lineRenderer.SetPosition(0, transform.position + transform.forward);//레이저 시작지점

        if (Physics.Raycast(transform.position, transform.forward, out hit, laserDistance))//레이캐스트를 써서 닿은 오브젝트 체크
        {
            lineRenderer.SetPosition(1, hit.point);//닿으면 도착점을 닿은지점으로 변경
            hitEffect.transform.position = hit.point;// 이펙트를 도착점에 옮기기

        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward * laserDistance);//닿지않으면 도착점을 최대거리만큼 
            hitEffect.transform.position = new Vector3(1000, 1000, 1000);//이펙트는 안보이게 멀리 보내버리기 껏다켰다하면 귀찮으니까
        }

        if (Input.GetKeyDown(KeyCode.Space)&& !isCatch)//물체를 잡는 코드
        {
            Catch();
        }
        else if (Input.GetKeyDown(KeyCode.Space)&& isCatch)//물체를 잡고있을때 발사하는 코드
        {
            Fire();
        }
    }

    public void Catch()
    {
        if (isPlayerShoot)//게임 클리어되면 발사 못하게
        {


            if (hit.collider != null)//레이저가 닿은물체는 실행
            {

                if (hit.collider.gameObject.GetComponent<CatchAble>() == null)//(오브젝트에 CatchAble스크립트가 있는지 확인)
                {
                    //없으면 실행
                    Debug.Log("집어지지않는 물체임");
                    hitObjScript = null;
                }
                else
                {

                    //있으면 실행
                    Debug.Log("충전");
                    hitObjScript = hit.collider.gameObject.GetComponent<CatchAble>();//물체의 스크립트 접근
                    hitObjScript.isPlayerEnter = true;//물체에 있는 bool 함수 활성화
                    isCatch = true;
                    hitEffect.GetComponent<MeshRenderer>().enabled = false;//잡은동안 이펙트 안나오게

                    fireButton.SetActive(true);
                    catchButton.SetActive(false);

                    if (hit.collider.tag == "Item")//아이템은 먹자마자 효과가 발생하니까 캐치 해제
                    {
                        isCatch = false;
                        hitEffect.GetComponent<MeshRenderer>().enabled = true;//아이템이니까 이펙트 다시
                        Debug.Log("아이템 획득");

                        fireButton.SetActive(false);
                        catchButton.SetActive(true);
                    }
                }
            }
            else
            {
                //빈공간에 체크함
                Debug.Log("No collision detected");
            }
        }
    }
    public void Fire()
    {
        if (isPlayerShoot)//게임 클리어되면 발사 못하게
        {


            Debug.Log("발사");
            attackSound.Play();
            hitObjScript.isPlayerEnter = false;//자식오브젝트 해제하는 bool 함수 활성화
            isCatch = false;
            hitEffect.GetComponent<MeshRenderer>().enabled = true;//발사하면 이펙트 다시
            hitObjScript = null; //날린 물체 접근 해제

            fireButton.SetActive(false);
            catchButton.SetActive(true);
        }
    }

}
