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
        for (float t = 0; t < 1; t += Time.deltaTime)//�������Ӹ��� ũ��
        {
            float s = curve.Evaluate(t);//�ش�Ǵ� �ð��� Ŀ�� ��ġ�� s�� �Ҵ�
            this.gameObject.transform.localScale = new Vector3(s * size, s * size, s * size);//s�� ũ�� ��
            yield return 0;//������ ���� �ۿ��� ������ �� �����ӿ� ������ ����Ǿ ȿ���� ���� 
        }
        StartCoroutine(Down());
    }

    IEnumerator Down()
    {
        for (float t = 1; t > 0; t -= Time.deltaTime)//�������Ӹ��� �۰�
        {
            float s = curve.Evaluate(t);//�ش�Ǵ� �ð��� Ŀ�� ��ġ�� s�� �Ҵ�
            this.gameObject.transform.localScale = new Vector3(s * size, s * size, s * size);//s�� ũ�� ��
            yield return 0;//������ ���� �ۿ��� ������ �� �����ӿ� ������ ����Ǿ ȿ���� ���� 
        }
        StartCoroutine(Up());
    }
}
