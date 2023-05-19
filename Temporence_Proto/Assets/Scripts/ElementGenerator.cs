using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGenerator : MonoBehaviour
{
    public GameObject flamePrefab;
    GameObject map;
    float span = 0.3f; //0.3��
    float delta = 0;

    void Start()
    {
        this.map = GameObject.Find("background 1");
    }
       
    void Update()
    {
        this.delta += Time.deltaTime; // �������ӿ��� ���� ������ ������ �ð� 
        if (this.delta > this.span) //���ϴ� �ð��� �������� ����
        {
            // Debug.Log("0.3��");
            this.delta = 0; //�ʱ�ȭ

            GameObject flame = Instantiate(flamePrefab) as GameObject; //�������� �̿��� �Ҳ��� �����ϴ� �ڵ带 �ش���ο� �ۼ��Ͻÿ�     
            flame.transform.position = GetRandomPosition(); //�ұ�Ģ�� ��ġ�� ����
        }   
    }

    
    //�Ʒ��� �� ���� ������ġ ���͸� ������� ���� �޼ҵ�
    private Vector3 GetRandomPosition()
    {
        RectTransform rectTran = map.GetComponent<RectTransform>(); //�ʿ� ������ rectTransform ������Ʈ ���

        //rectTransform �̿��� x, y�� ���� ��ǥ ���
        float posX = Random.Range(-rectTran.rect.width / 2f, rectTran.rect.width / 2f);
        float posY = Random.Range(-rectTran.rect.height / 2f, rectTran.rect.height / 2f);

        return new Vector3(posX, posY, 0); 
    }

}
