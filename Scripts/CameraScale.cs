using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    public GameObject stageLimit;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(stageLimit.transform.localScale.y);
        this.gameObject.transform.position = new Vector3(0, stageLimit.transform.localScale.y * 3, 0);//맵크기따라 카메라 거리 자동 변경 일단 3배하니까 딱맞음
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
