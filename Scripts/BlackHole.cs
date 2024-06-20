using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    public GameObject whiteHole;
    public GameObject blackHoleSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Untagged")//�÷��̾� �ݶ��̴��� ������
        {
            //PlayerMovement.player.transform.rotation = whiteHole.transform.rotation;
            PlayerMovement.player.transform.position = whiteHole.transform.position; // �÷��̾� �̵� �̷����ϴ� ���� �÷��̾�� �÷��̾� �ݶ��̴��� ���� �ٸ� �ڸ��� �־
            GameObject bSound = Instantiate(blackHoleSound, transform.position, Quaternion.identity);//��Ȧ ��� ȿ��
            Destroy(bSound, 5f);
        }
        else if(other.tag == "DeathWall" || other.tag == "OutsideWall")//����ó��
        {
            //�ٱ������� ȸ���Ҷ� �ε�ġ�� �Ǵµ� �����̸� �ȵǴϱ�
        }
        else
        {
            other.gameObject.transform.position = whiteHole.transform.position;

            GameObject bSound = Instantiate(blackHoleSound, transform.position, Quaternion.identity);//��Ȧ ��� ȿ��
            Destroy(bSound, 5f);
        }
    }

}
