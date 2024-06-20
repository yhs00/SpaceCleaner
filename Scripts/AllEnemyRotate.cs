using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemyRotate : MonoBehaviour
{
    public bool isRightRotate;
    public bool isLeftRotate;
    Quaternion nowRotation;
    Quaternion plusRightRotation;
    Quaternion plusLeftRotation;
    public GameObject playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        nowRotation = Quaternion.Euler(new Vector3(0, 0, 0)); //���� ȸ����
        plusRightRotation = Quaternion.Euler(new Vector3(0, 90, 0));//��ȸ��
        plusLeftRotation = Quaternion.Euler(new Vector3(0, -90, 0));//��ȸ��
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, nowRotation, Time.fixedDeltaTime); //������ nowRotation����ŭ ȸ��

        if (isRightRotate == true)//���� BreakAtive���� true�� �Ǹ�
        {
            StartCoroutine(PlayerColliderOff());
            nowRotation = nowRotation * plusRightRotation; // ���� ȸ������ ��ȸ������ ���ϱ�
            isRightRotate = false;
        }

        if (isLeftRotate == true)//���� BreakAtive���� true�� �Ǹ�
        {
            StartCoroutine(PlayerColliderOff());
            nowRotation = nowRotation * plusLeftRotation; // ���� ȸ������ ��ȸ������ ���ϱ�
            isLeftRotate = false;
        }
    }

    IEnumerator PlayerColliderOff()//�̰��ϴ�����:���ϸ� ȭ��ȸ���Ҷ� �÷��̾�� �˴� �ε���
    {
        PlayerMovement.isPlayerMove = false;//ȸ���ϴ� ���� �÷��̾��� ������ ����
        PlayerLaser.isPlayerShoot = false;//ȸ���ϴ� ���� �÷��̾��� �߻� ����
        playerCollider.SetActive(false);
        yield return new WaitForSeconds(1f);
        PlayerMovement.isPlayerMove = true;//ȸ���������� �ٽ� �����̰�
        PlayerLaser.isPlayerShoot = true;
        playerCollider.SetActive(true);
    }
}
