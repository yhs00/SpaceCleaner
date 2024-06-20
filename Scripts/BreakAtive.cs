using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAtive : MonoBehaviour
{
    

    public int health;
    public bool isEnemy;
    public bool isBAWall;
    public GameObject dieEffect;
    public GameObject destroySound;
    public bool triggerAtive;


    // Start is called before the first frame update
    void Start()
    {
        triggerAtive = false;
        isEnemy = false;

        health = 1;//�⺻ü����1 �μ����� ���̳� ū ���ʹ̴� ü���� �߰�?
        if(this.gameObject.tag == "Enemy")//�� ���� Ȯ�� �ϱ�
        {
            GameManager.allEnemyCount++;//���̶�� ���ӸŴ����� �ִ� ī��Ʈ 1 �ø��� �׷��� ��ü �� ���� Ȯ��
            isEnemy = true;
        }

        if(isBAWall)//�μ����º�����Ȯ��
        {
            GameManager.allBAWallCount++;//�μ����º��̶�� ���ӸŴ����� �ִ� ī��Ʈ 1 �ø��� �׷��� ��ü�μ����º� ���� Ȯ��
        }
    }

    // Update is called once per frame
    void Update()
    {
        die();
    }

    void die()
    {
        if (health <= 0)
        {

            if (isEnemy)//���� ���̶�� ����
            {
                GameManager.allEnemyCount--; //������ ���ӸŴ����� �ִ� allEnemyCount 1 ����
            }

            if(isBAWall)
            {
                GameManager.allBAWallCount--;
            }


            GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);//�ı�ȿ��
            Destroy(effect, 5f);
            GameObject dieSound = Instantiate(destroySound, transform.position, Quaternion.identity);//�ı�����ȿ��
            Destroy(dieSound, 5f);
            gameObject.SetActive(false);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(triggerAtive)//�̰��� ����: AllEnemy�� ȸ���ϸ� ������ �ٱ����� �������� �ִµ� �߻� �Ǿ������� Ȱ��ȭ�ؾ��ϴϱ�
        {
            if (other.tag == "BlackHole" || other.tag == "OutsideWall")//��Ż�̳� �ٱ� ���� �ƴѰſ� ������ �ı�
            {
                Debug.Log("asdfqwdf");
                return;
            }

            if (other.tag == "DeathWall")//�ʹ��� ����� �ı��� �Ұ����ϴϱ� ���ӿ��� ó��
            {
                GameManager.isSomethingOut = true;
                return;
            }

            health--;
            if(other.GetComponent<BreakAtive>() != null)//(������Ʈ�� BreakAtive��ũ��Ʈ�� �ִ��� Ȯ��)
            {
                //������ ����
                other.GetComponent<BreakAtive>().health--;
            }

        }
    }
}
