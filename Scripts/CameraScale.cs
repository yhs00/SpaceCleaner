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
        this.gameObject.transform.position = new Vector3(0, stageLimit.transform.localScale.y * 3, 0);//��ũ����� ī�޶� �Ÿ� �ڵ� ���� �ϴ� 3���ϴϱ� ������
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
