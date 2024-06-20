using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameObject player;
    public static int playerHealth;
    public static float moveSpeed = 5f;
    public static bool isPlayerMove;

    public static bool isPlayerRightRotate;
    public static bool isPlayerLeftRotate;

    Quaternion playerRotation;

    public GameObject dieEffect;
    public GameObject playerDestroySound;

    public Rigidbody rb;

    Vector3 movement;
    public Joystick joystick;

    Vector3 wallCheck;
    bool a;
    void Start()
    {
        player = this.gameObject;//�÷��̾� �ڽ��� ����ƽ���� �ٲٱ�
        playerHealth = 1;
        playerRotation = Quaternion.Euler(new Vector3(0,this.gameObject.transform.rotation.eulerAngles.y,0)); //���� �÷��̾��� ȸ����
        isPlayerMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") + joystick.Horizontal;//�¿��̵�(Ű�����Է� + ��ġ�Է�)
        movement.z = Input.GetAxisRaw("Vertical") + joystick.Vertical;//�����̵�

        PlayerRotate();
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, Time.fixedDeltaTime); //������ nowRotation����ŭ ȸ��

        Die();
    }

    void FixedUpdate()
    {
        if(isPlayerMove)
        {
            PlayerMove();
        }
    }

    void PlayerMove()
    {
        //�̵�
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = Vector3.zero;//�̰��ؾ� �ε������� �� ƨ�ܳ���

    }

    void PlayerRotate()//�÷��̾� ȸ�� �������� ������ ����
    {
        if(isPlayerRightRotate)
        {
            playerRotation = playerRotation * Quaternion.Euler(new Vector3(0, 90, 0));
            isPlayerRightRotate = false;
            
        }

        if(isPlayerLeftRotate)
        {
            playerRotation = playerRotation * Quaternion.Euler(new Vector3(0, -90, 0));
            isPlayerLeftRotate = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "Item" || other.tag == "Laser")//���̳� ������,�������� ������
        {
            other.GetComponent<BreakAtive>().health--;//������ü�°�
            playerHealth--;//�÷��̾�ü���� ����
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "OutsideWall")//��������� �ڵ�1
        {
            wallCheck = collision.gameObject.transform.position - transform.position;//���� �浹�ϸ� ���� ���� ��ġ�� ���
            if(a == false)
            {
                if (wallCheck.z > 0)//���� ������ ���� ������
                {
                    transform.Translate(new Vector3(0, 0, -0.05f) * Time.fixedDeltaTime * 10, Space.World);//�Ʒ��� ��¦
                    Debug.Log("�Ʒ���");

                }
                else//���� ������ �Ʒ��� ������
                {
                    transform.Translate(new Vector3(0, 0, 0.05f) * Time.fixedDeltaTime * 10, Space.World);//���� ��¦
                    Debug.Log("����");

                }
                a = true;
            }
            else
            {
                if (wallCheck.x > 0)//���� ������ �����ʿ� ������
                {
                    transform.Translate(new Vector3(-0.05f, 0, 0) * Time.fixedDeltaTime * 10, Space.World);//�������� ��¦
                    Debug.Log("����");


                }
                else//���� ������ ���ʿ� ������
                {
                    transform.Translate(new Vector3(0.05f, 0, 0) * Time.fixedDeltaTime * 10, Space.World);//�����ʷ� ��¦
                    Debug.Log("������");
                }
                a = false;
            }

        }
    }


    public void Die()//�����
    {
        if (playerHealth <= 0)
        {
            GameManager.isPlayerDie = true;//���ӸŴ����� �÷��̾� ����� �˷��ֱ�
            GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);//����� �ı�ȿ��
            Destroy(effect, 5f); 
            GameObject dieSound = Instantiate(playerDestroySound, transform.position, Quaternion.identity);//�ı�����ȿ��
            Destroy(dieSound, 5f);
            gameObject.SetActive(false);//�ı��Ǵϱ� ��Ȱ��ȭ
        }
    }

}
