using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CatchAble : MonoBehaviour
{
    public bool isPlayerEnter;
    public bool isFireReady;
    public bool isRightItem;
    public bool isLeftItem;

    public bool isPlayerRightItem;
    public bool isPlayerLeftItem;

    public bool isBattery;

    public AllEnemyRotate allEnemyRotate;

    public Vector3 playerLook;

    public GameObject catchSound;

    private void Awake()
    {
        isPlayerEnter = false;
        isFireReady = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        allEnemyRotate = transform.parent.gameObject.GetComponent<AllEnemyRotate>(); //�����Ҷ� AllEnemyRotate�� ��ũ��Ʈ ����

        if (isBattery)//���͸� ���� Ȯ���ϱ�
        {
            GameManager.allBatteryCount++;//���͸���� ���ӸŴ����� �ִ� ī��Ʈ 1 �ø��� �׷��� ��ü ���͸� ���� Ȯ��
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerEnter == true)//�÷��̾�� Ʈ�簡 �Ǹ� �ڽĿ�����Ʈ�� ����
        {
            if(!isFireReady) //�ѹ��� ����
            {
                Sound();
                gameObject.GetComponent<Collider>().enabled = false;//���� �������� �߻����� �浹�� �ı����� �ʰ� �ݶ��̴� ��Ȱ��ȭ
                transform.SetParent(PlayerMovement.player.transform);//�÷��̾ �θ��
                transform.localScale = Vector3.one * 0.5f;
                isFireReady = true;//�߻簡��

            }
            else //�ݺ� ����
            {
                transform.localPosition = Vector3.forward * PlayerLaser.laserDistance;//�߻���ġ�� �÷��̾� ������
                transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime * 10);//��Ҵٴ� ǥ�÷� ������ ȸ��
            }

            if (this.gameObject.tag == "Item")//���� ������ �������̶��
            {
                Item();
            }


        }

        if (!isPlayerEnter&& isFireReady)//�÷��̾�� false�� �Ǹ� ����// ���� ������ ���ӽ��ۺ��� �߻�Ǳ⶧�� 
        {

            if(transform.parent != null)//�ѹ��� ����
            {
                playerLook = PlayerMovement.player.transform.forward; //�÷��̾��� ���� Ȯ��
                transform.parent = null;
            }//�̰� �ϴ� ����: ���ϸ� �߻��� �÷��̾ ȸ�������� �߻�� ������Ʈ�� ���� ȸ����

            gameObject.GetComponent<Collider>().enabled = true;//���� �������� �߻��� �浹�� �ı��ǰ� �ݶ��̴� Ȱ��ȭ
            gameObject.GetComponent<BreakAtive>().triggerAtive = true;//BreakAtive.triggerAtive Ȱ��ȭ
            transform.Translate(playerLook * Time.deltaTime * 10, Space.World);// �÷��̾� �������� ������ �߻�
            
        }

    }

    void Item()
    {
        if (isRightItem)//���� ��ü�� ��ȸ���������̸�
        {
            allEnemyRotate.isRightRotate = true; // allEnemyRotate�Լ����� isRightRotate�� true�� �ٲٱ�
            Debug.Log("��ȸ�� ȹ��!");
        }


        if (isLeftItem)//���� ��ü�� ��ȸ���������̸�
        {
            allEnemyRotate.isLeftRotate = true; // allEnemyRotate�Լ����� isLeftRotate�� true�� �ٲٱ�
            Debug.Log("��ȸ�� ȹ��!");
        }

        if(isBattery)
        {
            GameManager.allBatteryCount--;//������ϱ� ī��Ʈ 1����
            Debug.Log("���͸� ȹ��!");
        }


        if(isPlayerRightItem)
        {
            PlayerMovement.isPlayerRightRotate = true;
            Debug.Log("�÷��̾� ��ȸ��!");
        }

        if (isPlayerLeftItem)
        {
            PlayerMovement.isPlayerLeftRotate = true;
            Debug.Log("�÷��̾� ��ȸ��!");
        }

        Sound();
        gameObject.SetActive(false);
    }

    void Sound()
    {
        GameObject cSound = Instantiate(catchSound, transform.position, Quaternion.identity);//��������ȿ�� ��,������,���͸� �ٸ��� ������ ��
        Destroy(cSound, 5f);
    }
}
