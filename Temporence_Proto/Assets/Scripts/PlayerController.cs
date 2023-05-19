using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    //아래 변수에 자기자신 할당 해주어야 함 
    public Rigidbody2D RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text IDText;
    public Image LifeHeartImage;

    bool isGround; //점프 계산용
    Vector3 curPos; 
    float moveSpeed = 5.0f; //이동 속도

    //음향효과
    public AudioClip clearSound;
    AudioSource audioSource;  

    void Awake()
    {
        /* 닉네임 */
        IDText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; // 자신이면 : 다른 사람이면
        IDText.color = PV.IsMine ? Color.green : Color.red;
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //포톤뷰가 자신의 뷰인 경우 컨트롤 가능
        if (PV.IsMine)
        {
            /* 상하좌우 이동 */
            float axisX = Input.GetAxisRaw("Horizontal");   //좌우로(방향키 왼쪽/오른쪽) 움직이는 값을 받아옴
            float axisY = Input.GetAxisRaw("Vertical");     //수직으로(방향키 위/아래) 움직이는 값을 받아옴
 
            //리기드바디를 이용해 이동 
            RB.velocity = new Vector2(moveSpeed * axisX, RB.velocity.y);//moveSpeed * axisY

            /* 상하좌우 애니메이션 */
            if (axisX != 0)
            {
                AN.SetBool("walk", true);
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, axisX);// flipX 를 동기화
            }
            else AN.SetBool("walk",false);

            // ↑ 점프, 바닥체크
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AN.SetBool("jump", true);
                PV.RPC("JumpRPC", RpcTarget.All);
            }
        } // 타인 플레이어는 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos; //너무 많이 떨어져 있는 경우 순간이동
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10); //부드럽게 이동 

    }


    [PunRPC]
    void FlipXRPC(float axis) => SR.flipX = axis == -1; //누르는 방향에 따라 flipX bool 바꿔줌

    //플레이어 삭제
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
        //  Debug.Log("닿고있음");
        // 스페이스 잡기 
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
        /* 위치 , 체력 변수 동기화 */
        if (stream.IsWriting)
        {
            //PV가 isMine 인 경우 넘겨줌
            stream.SendNext(transform.position);
            stream.SendNext(LifeHeartImage.fillAmount);
        }
        else
        {
            //PV가 isMine 아닌경우 받는곳
            curPos = (Vector3)stream.ReceiveNext();
            LifeHeartImage.fillAmount = (float)stream.ReceiveNext();
        }
       
    }
}
