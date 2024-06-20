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

        health = 1;//기본체력은1 부서지는 벽이나 큰 에너미는 체력을 추가?
        if(this.gameObject.tag == "Enemy")//적 갯수 확인 하기
        {
            GameManager.allEnemyCount++;//적이라면 게임매니저에 있는 카운트 1 올리기 그래서 전체 적 갯수 확인
            isEnemy = true;
        }

        if(isBAWall)//부서지는벽갯수확인
        {
            GameManager.allBAWallCount++;//부서지는벽이라면 게임매니저에 있는 카운트 1 올리기 그래서 전체부서지는벽 갯수 확인
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

            if (isEnemy)//만약 적이라면 실행
            {
                GameManager.allEnemyCount--; //죽으면 게임매니저에 있는 allEnemyCount 1 감소
            }

            if(isBAWall)
            {
                GameManager.allBAWallCount--;
            }


            GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);//파괴효과
            Destroy(effect, 5f);
            GameObject dieSound = Instantiate(destroySound, transform.position, Quaternion.identity);//파괴사운드효과
            Destroy(dieSound, 5f);
            gameObject.SetActive(false);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(triggerAtive)//이거한 이유: AllEnemy가 회전하면 적들이 바깥벽에 닿을때가 있는데 발사 되었을때만 활성화해야하니까
        {
            if (other.tag == "BlackHole" || other.tag == "OutsideWall")//포탈이나 바깥 벽이 아닌거에 닿으면 파괴
            {
                Debug.Log("asdfqwdf");
                return;
            }

            if (other.tag == "DeathWall")//맵밖을 벗어나면 파괴가 불가능하니까 게임오버 처리
            {
                GameManager.isSomethingOut = true;
                return;
            }

            health--;
            if(other.GetComponent<BreakAtive>() != null)//(오브젝트에 BreakAtive스크립트가 있는지 확인)
            {
                //있으면 실행
                other.GetComponent<BreakAtive>().health--;
            }

        }
    }
}
