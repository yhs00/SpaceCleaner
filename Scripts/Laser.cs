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

        lineRenderer.positionCount = 2;//������,������2���ʿ��ϴϱ� 2

        
    }
    public void Update()
    {
        lineRenderer.SetPosition(0, transform.position);//������ ��������

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLaserDistance))//����ĳ��Ʈ�� �Ἥ ���� ������Ʈ üũ
        {
            lineRenderer.SetPosition(1, hit.point);//������ �������� ������������ ����
            laserEffect.transform.position = hit.point;//������ ����Ʈ�� �������� �ű��


            //�̰� �� ����: ȸ���ϴ� ���� ���׷� ������ ��ó�� �ִ� ������Ʈ�� �ı���, ���� ���� ��� ���ִ� �뵵�� ���
            //�� PlayerMovement.isPlayerMove�ĸ� ���µ��� �÷��̾�갡 ������ ������
            if (PlayerMovement.isPlayerMove)
            {
                LaserBeam();
            }

        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward * maxLaserDistance);//���������� �������� �ִ�Ÿ���ŭ 
            laserEffect.transform.position = new Vector3(1000, 1000, 1000);//����������Ʈ�� �Ⱥ��̰� �ָ� ���������� �����״��ϸ� �������ϱ�
        }

    }

    public void LaserBeam()
    {   
        if (hit.collider.tag == "Untagged")//���� �÷��̾ �ִ� �ڽ� ������Ʈ�� �ݶ��̴��� ������(�÷��̾ �ݶ��̴��� ��� �س��� ���)
        {
            PlayerMovement.playerHealth--;//�÷��̾� ü�� ����(�ı�)
        }

        if (hit.collider.tag == "Enemy" || hit.collider.tag == "Item")//���̳� �������̶��
        {
            hit.collider.gameObject.GetComponent<BreakAtive>().health--;//�� ��ü�� ü�°���
        }

    }

}
