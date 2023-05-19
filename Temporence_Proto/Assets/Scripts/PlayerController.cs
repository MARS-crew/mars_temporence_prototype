using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    //�Ʒ� ������ �ڱ��ڽ� �Ҵ� ���־�� �� 
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text IDText;
    public Image LifeHeartImage;

    bool isGround; //���� ����
    Vector3 curPos; 
    float moveSpeed = 5.0f; //�̵� �ӵ�

    //����ȿ��
    public AudioClip clearSound;
    AudioSource audioSource;  

    void Awake()
    {
        /* �г��� */
        IDText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; // �ڽ��̸� : �ٸ� ����̸�
        IDText.color = PV.IsMine ? Color.green : Color.red;
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //����䰡 �ڽ��� ���� ��� ��Ʈ�� ����
        if (PV.IsMine)
        {
            /* �����¿� �̵� */
            float axisX = Input.GetAxisRaw("Horizontal");   //�¿��(����Ű ����/������) �����̴� ���� �޾ƿ�
            float axisY = Input.GetAxisRaw("Vertical");     //��������(����Ű ��/�Ʒ�) �����̴� ���� �޾ƿ�
 
            //�����ٵ� �̿��� �̵� 
            RB.velocity = new Vector2(moveSpeed * axisX, RB.velocity.y);//moveSpeed * axisY

            /* �����¿� �ִϸ��̼� */
            if (axisX != 0)
            {
                AN.SetBool("walk", true);
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, axisX);// flipX �� ����ȭ
            }
            else AN.SetBool("walk",false);

            // �� ����, �ٴ�üũ
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AN.SetBool("jump", true);
                PV.RPC("JumpRPC", RpcTarget.All);
            }
        } // Ÿ�� �÷��̾�� �ε巴�� ��ġ ����ȭ
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos; //�ʹ� ���� ������ �ִ� ��� �����̵�
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10); //�ε巴�� �̵� 

    }


    [PunRPC]
    void FlipXRPC(float axis) => SR.flipX = axis == -1; //������ ���⿡ ���� flipX bool �ٲ���

    //�÷��̾� ����
    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    [PunRPC]
    void JumpRPC()
    {
        RB.velocity = Vector2.zero;
        RB.AddForce(Vector2.up * 700);
    }
  
    void Hit()
    {
        LifeHeartImage.fillAmount -= 0.5f;
        if (LifeHeartImage.fillAmount <= 0)
        {
            GameObject.Find("Canvas").transform.Find("RespawnPanel").gameObject.SetActive(true);
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //  Debug.Log("�������");
        // �����̽� ��� 
        if(col.gameObject.CompareTag("Player")){
            if (Input.GetKeyDown(KeyCode.Space))
            {
   
            }
        }
    }

   void OnCollisionEnter2D(Collision2D col)
    {  
        if (col.gameObject.CompareTag("Wall")) 
        {

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /* ��ġ , ü�� ���� ����ȭ */
        if (stream.IsWriting)
        {
            //PV�� isMine �� ��� �Ѱ���
            stream.SendNext(transform.position);
            stream.SendNext(LifeHeartImage.fillAmount);
        }
        else
        {
            //PV�� isMine �ƴѰ�� �޴°�
            curPos = (Vector3)stream.ReceiveNext();
            LifeHeartImage.fillAmount = (float)stream.ReceiveNext();
        }
       
    }
}
