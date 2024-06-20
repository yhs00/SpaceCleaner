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


        lineRenderer.positionCount = 2;//������,������2���ʿ��ϴϱ� 2
    }

    // Update is called once per frame
    void Update()
    {
        

        lineRenderer.SetPosition(0, transform.position + transform.forward);//������ ��������

        if (Physics.Raycast(transform.position, transform.forward, out hit, laserDistance))//����ĳ��Ʈ�� �Ἥ ���� ������Ʈ üũ
        {
            lineRenderer.SetPosition(1, hit.point);//������ �������� ������������ ����
            hitEffect.transform.position = hit.point;// ����Ʈ�� �������� �ű��

        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward * laserDistance);//���������� �������� �ִ�Ÿ���ŭ 
            hitEffect.transform.position = new Vector3(1000, 1000, 1000);//����Ʈ�� �Ⱥ��̰� �ָ� ���������� �����״��ϸ� �������ϱ�
        }

        if (Input.GetKeyDown(KeyCode.Space)&& !isCatch)//��ü�� ��� �ڵ�
        {
            Catch();
        }
        else if (Input.GetKeyDown(KeyCode.Space)&& isCatch)//��ü�� ��������� �߻��ϴ� �ڵ�
        {
            Fire();
        }
    }

    public void Catch()
    {
        if (isPlayerShoot)//���� Ŭ����Ǹ� �߻� ���ϰ�
        {


            if (hit.collider != null)//�������� ������ü�� ����
            {

                if (hit.collider.gameObject.GetComponent<CatchAble>() == null)//(������Ʈ�� CatchAble��ũ��Ʈ�� �ִ��� Ȯ��)
                {
                    //������ ����
                    Debug.Log("���������ʴ� ��ü��");
                    hitObjScript = null;
                }
                else
                {

                    //������ ����
                    Debug.Log("����");
                    hitObjScript = hit.collider.gameObject.GetComponent<CatchAble>();//��ü�� ��ũ��Ʈ ����
                    hitObjScript.isPlayerEnter = true;//��ü�� �ִ� bool �Լ� Ȱ��ȭ
                    isCatch = true;
                    hitEffect.GetComponent<MeshRenderer>().enabled = false;//�������� ����Ʈ �ȳ�����

                    fireButton.SetActive(true);
                    catchButton.SetActive(false);

                    if (hit.collider.tag == "Item")//�������� ���ڸ��� ȿ���� �߻��ϴϱ� ĳġ ����
                    {
                        isCatch = false;
                        hitEffect.GetComponent<MeshRenderer>().enabled = true;//�������̴ϱ� ����Ʈ �ٽ�
                        Debug.Log("������ ȹ��");

                        fireButton.SetActive(false);
                        catchButton.SetActive(true);
                    }
                }
            }
            else
            {
                //������� üũ��
                Debug.Log("No collision detected");
            }
        }
    }
    public void Fire()
    {
        if (isPlayerShoot)//���� Ŭ����Ǹ� �߻� ���ϰ�
        {


            Debug.Log("�߻�");
            attackSound.Play();
            hitObjScript.isPlayerEnter = false;//�ڽĿ�����Ʈ �����ϴ� bool �Լ� Ȱ��ȭ
            isCatch = false;
            hitEffect.GetComponent<MeshRenderer>().enabled = true;//�߻��ϸ� ����Ʈ �ٽ�
            hitObjScript = null; //���� ��ü ���� ����

            fireButton.SetActive(false);
            catchButton.SetActive(true);
        }
    }

}
