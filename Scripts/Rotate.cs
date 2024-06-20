using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 5 * Time.deltaTime);//배경과 꾸미기 오브젝트 이쁘게 하려고
    }
}
