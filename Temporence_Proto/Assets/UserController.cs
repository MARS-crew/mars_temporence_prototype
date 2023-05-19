using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
using Photon.Pun;
using Photon.Realtime;
//using Cinemachine;

public class UserController : MonoBehaviourPunCallbacks, IPunObservable
{
    //�Ʒ� ������ �ڱ��ڽ� �Ҵ� ���־�� �� 
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text IDText;
    public Image LifeHeartImage;

    bool isGround; //���� ����
    Vector3 curPos; //

    //
    private float moveSpeed = 5.0f;                 //�̵� �ӵ�
    private Vector3 moveDirection = Vector3.zero;   //�̵� ����
    private int life = 5;
    GameObject director;

    //����ȿ��
    public AudioClip clearSound;
    AudioSource audioSource;  //AudioSource ������ audioSource ���� ����

    void Awake()
    {
        // �г���
        IDText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; // �ڽ��̸� t : �ٸ� ����̸� t
        //IDText.color = PV.IsMine ? Color.green : Color.red;

        if (PV.IsMine)
        {
            // 2D ī�޶�
           // var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
         //   CM.Follow = transform;
          //  CM.LookAt = transform;
        }
    }

    private void Start()
    {
        Camera cam = Camera.main;
        cam.transform.SetParent(transform);
        director = GameObject.Find("gameDirector");
        //  cam.transform.localPosition = new Vector3(0f, 0f, -10f);
        //    cam.orthographicSize = 2.5f;//���� ǥ�õǴ� ȭ�� ������ ����

        audioSource = gameObject.GetComponent<AudioSource>();


    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");   //Horizontal : �¿��(����Ű ����/������) �����̴� ���� �޾ƿ�
        float y = Input.GetAxisRaw("Vertical");     //Vertical : ��������(����Ű ��/�Ʒ�) �����̴� ���� �޾ƿ�

  
        //�̵� ���� ����
        //z���� 0���� �����Ǿ� 2���� �������� �ǹ�
        moveDirection = new Vector3(x, y, 0);
        //���� ��ġ + (���� * �ӵ�)

        //Time.deltaTime�� ������ FPS���� ������ ��
        if (!(x== 0 && y == 0))
        {

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        //�̵� ���� �ֿܼ� ���
        if (Input.GetKeyDown(KeyCode.A))
        {
          //  Debug.Log("���� �̵�");
          
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
           // Debug.Log("������ �̵�");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
        //    Debug.Log("���� �̵�");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
         //   Debug.Log("�Ʒ��� �̵�");
        }

        //ȭ���� Ŭ���ϸ� 10f �ӵ��� ĳ���� ȸ��
        if (Input.GetMouseButton(0))    //��Ŭ�� ���������� ȸ�� / 0�� ��Ŭ�� �ǹ�
        {
          
        }
        if (Input.GetMouseButton(1))    //��Ŭ�� �������� ȸ�� / 1�� ��Ŭ�� �ǹ�
        {
           
        }
    }

    //�浹
    void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Flame") //�浹�� ������Ʈ�� �±װ� Flag�� ��쿡�� ����
        {
            life -= 1;
            director.GetComponent<gameDirector>().DecreaseLife(life);
        }
 

        if (collision.gameObject.tag == "Flag") //�浹�� ������Ʈ�� �±װ� Flag�� ��쿡�� ����
        {
            audioSource.PlayOneShot(clearSound);   //AudioSource ������Ʈ�� �̿��Ͽ� sound������ ����� AudioClip ���
        }
    }

    //�������̽� ���� 
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
          //  stream.SendNext(HealthImage.fillAmount);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
       //     HealthImage.fillAmount = (float)stream.ReceiveNext();
        }
    }
}
