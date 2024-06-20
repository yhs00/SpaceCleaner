using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStop : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        transform.parent.gameObject.GetComponent<Laser>().maxLaserDistance = 0f;
    }

    private void OnTriggerExit(Collider other)
    {
        transform.parent.gameObject.GetComponent<Laser>().maxLaserDistance = 100f;
    }

    //이 스크립트는 플레이어가 레이저를 들고 벽에 가까이 가면 레이저가 벽을 통과해서 발사되는 버그때문에 만들어짐
    //레이저오브젝트안에 작은 콜라이더를 따로 만들어서 이 작은 콜라이더가 벽에 들어가면 레이저 최대 사거리를 0으로 만들어 발사가 안되게 만듦
}
