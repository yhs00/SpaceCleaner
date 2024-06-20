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

    //�� ��ũ��Ʈ�� �÷��̾ �������� ��� ���� ������ ���� �������� ���� ����ؼ� �߻�Ǵ� ���׶����� �������
    //������������Ʈ�ȿ� ���� �ݶ��̴��� ���� ���� �� ���� �ݶ��̴��� ���� ���� ������ �ִ� ��Ÿ��� 0���� ����� �߻簡 �ȵǰ� ����
}
