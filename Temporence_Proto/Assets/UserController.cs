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
    //아래 변수에 자기자신 할당 해주어야 함 
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text IDText;
    public Image LifeHeartImage;

    bool isGround; //점프 계산용
    Vector3 curPos; //

    //
    private float moveSpeed = 5.0f;                 //이동 속도
    private Vector3 moveDirection = Vector3.zero;   //이동 방향
    private int life = 5;
    GameObject director;

    //음향효과
    public AudioClip clearSound;
    AudioSource audioSource;  //AudioSource 형식의 audioSource 변수 선언

    void Awake()
    {
        // 닉네임
        IDText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; // 자신이면 t : 다른 사람이면 t
        //IDText.color = PV.IsMine ? Color.green : Color.red;

        if (PV.IsMine)
        {
            // 2D 카메라
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
        //    cam.orthographicSize = 2.5f;//앱이 표시되는 화면 높이의 절반

        audioSource = gameObject.GetComponent<AudioSource>();


    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");   //Horizontal : 좌우로(방향키 왼쪽/오른쪽) 움직이는 값을 받아옴
        float y = Input.GetAxisRaw("Vertical");     //Vertical : 수직으로(방향키 위/아래) 움직이는 값을 받아옴

  
        //이동 방향 설정
        //z값이 0으로 설정되어 2차원 움직임을 의미
        moveDirection = new Vector3(x, y, 0);
        //현재 위치 + (방향 * 속도)

        //Time.deltaTime은 동일한 FPS값을 나오게 함
        if (!(x== 0 && y == 0))
        {

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        //이동 방향 콘솔에 출력
        if (Input.GetKeyDown(KeyCode.A))
        {
          //  Debug.Log("왼쪽 이동");
          
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
           // Debug.Log("오른쪽 이동");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
        //    Debug.Log("위로 이동");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
         //   Debug.Log("아래로 이동");
        }

        //화면을 클릭하면 10f 속도로 캐릭터 회전
        if (Input.GetMouseButton(0))    //좌클릭 오른쪽으로 회전 / 0은 좌클릭 의미
        {
          
        }
        if (Input.GetMouseButton(1))    //우클릭 왼쪽으로 회전 / 1은 우클릭 의미
        {
           
        }
    }

    //충돌
    void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Flame") //충돌한 오브젝트의 태그가 Flag인 경우에만 실행
        {
            life -= 1;
            director.GetComponent<gameDirector>().DecreaseLife(life);
        }
 

        if (collision.gameObject.tag == "Flag") //충돌한 오브젝트의 태그가 Flag인 경우에만 실행
        {
            audioSource.PlayOneShot(clearSound);   //AudioSource 컴포넌트를 이용하여 sound변수에 저장된 AudioClip 재생
        }
    }

    //인터페이스 구현 
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
