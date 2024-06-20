using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer lineRenderer;
    public GameObject laserEffect;
    public float maxLaserDistance = 100f;
    


    public void Start()
    {

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
    public void Update()
    {
        lineRenderer.SetPosition(0, transform.position);//레이저 시작지점

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLaserDistance))//레이캐스트를 써서 닿은 오브젝트 체크
        {
            lineRenderer.SetPosition(1, hit.point);//닿으면 도착점을 닿은지점으로 변경
            laserEffect.transform.position = hit.point;//레이저 이펙트를 도착점에 옮기기


            //이거 한 이유: 회전하는 동안 버그로 레이저 근처에 있는 오브젝트를 파괴함, 도는 동안 잠깐 꺼주는 용도로 사용
            //왜 PlayerMovement.isPlayerMove냐면 도는동안 플레이어무브가 꺼지기 때문에
            if (PlayerMovement.isPlayerMove)
            {
                LaserBeam();
            }

        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward * maxLaserDistance);//닿지않으면 도착점을 최대거리만큼 
            laserEffect.transform.position = new Vector3(1000, 1000, 1000);//레이저이펙트는 안보이게 멀리 보내버리기 껏다켰다하면 귀찮으니까
        }

    }

    public void LaserBeam()
    {   
        if (hit.collider.tag == "Untagged")//만약 플레이어에 있는 자식 오브젝트의 콜라이더에 닿으면(플레이어에 콜라이더가 없어서 해놓은 방식)
        {
            PlayerMovement.playerHealth--;//플레이어 체력 감소(파괴)
        }

        if (hit.collider.tag == "Enemy" || hit.collider.tag == "Item")//적이나 아이템이라면
        {
            hit.collider.gameObject.GetComponent<BreakAtive>().health--;//그 물체의 체력감소
        }

    }

}
