using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public AnimationCurve curve;
    public float size;

    public void Start()
    {
        StartCoroutine(Up());
    }

    IEnumerator Up()
    {
        for (float t = 0; t < 1; t += Time.deltaTime)//매프레임마다 크게
        {
            float s = curve.Evaluate(t);//해당되는 시간의 커브 수치를 s에 할당
            this.gameObject.transform.localScale = new Vector3(s * size, s * size, s * size);//s는 크기 값
            yield return 0;//리턴을 포문 밖에다 넣으면 한 프레임에 여러번 실행되어서 효과가 없음 
        }
        StartCoroutine(Down());
    }

    IEnumerator Down()
    {
        for (float t = 1; t > 0; t -= Time.deltaTime)//매프레임마다 작게
        {
            float s = curve.Evaluate(t);//해당되는 시간의 커브 수치를 s에 할당
            this.gameObject.transform.localScale = new Vector3(s * size, s * size, s * size);//s는 크기 값
            yield return 0;//리턴을 포문 밖에다 넣으면 한 프레임에 여러번 실행되어서 효과가 없음 
        }
        StartCoroutine(Up());
    }
}
