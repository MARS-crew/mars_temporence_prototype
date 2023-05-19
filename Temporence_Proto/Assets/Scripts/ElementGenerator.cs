using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGenerator : MonoBehaviour
{
    public GameObject flamePrefab;
    GameObject map;
    float span = 0.3f; //0.3초
    float delta = 0;

    void Start()
    {
        this.map = GameObject.Find("background 1");
    }
       
    void Update()
    {
        this.delta += Time.deltaTime; // 앞프레임에서 현재 프레임 사이의 시간 
        if (this.delta > this.span) //원하는 시간이 지났으면 실행
        {
            // Debug.Log("0.3초");
            this.delta = 0; //초기화

            GameObject flame = Instantiate(flamePrefab) as GameObject; //프리팹을 이용해 불꽃을 생성하는 코드를 해당라인에 작성하시오     
            flame.transform.position = GetRandomPosition(); //불규칙한 위치에 생성
        }   
    }

    
    //아래는 맵 내부 랜덤위치 벡터를 얻기위해 만든 메소드
    private Vector3 GetRandomPosition()
    {
        RectTransform rectTran = map.GetComponent<RectTransform>(); //맵에 부착된 rectTransform 컴포넌트 얻기

        //rectTransform 이용해 x, y축 랜덤 좌표 얻기
        float posX = Random.Range(-rectTran.rect.width / 2f, rectTran.rect.width / 2f);
        float posY = Random.Range(-rectTran.rect.height / 2f, rectTran.rect.height / 2f);

        return new Vector3(posX, posY, 0); 
    }

}
