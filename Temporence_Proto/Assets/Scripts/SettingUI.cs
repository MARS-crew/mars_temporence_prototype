using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    //리소스 가져오기
    [SerializeField] //유니티에서 적용 가능하도록 
    private Button KeyboardMouseSelectButton; 
    [SerializeField]
    private Button ControlpadSelectButton;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //현재값 저장되도록 함
        //플레이어 세팅 클래스의 controltype 에 따라 현재 활성화된 버튼을
        switch (PlayerSetting.controlType)
        {
            case EControlType.KeyboardMouse:
                KeyboardMouseSelectButton.image.color =Color.green;
                ControlpadSelectButton.image.color = Color.white;
                break;
            case EControlType.ControlPad:
                KeyboardMouseSelectButton.image.color = Color.white;
                ControlpadSelectButton.image.color = Color.green;
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void SetControlMode(int controlType)
    {
        //두가지 조작 셀렉트 버튼 클릭시 실행
        //숫자에 따라 조작모드 변경
        PlayerSetting.controlType = (EControlType)controlType;/////
        switch (PlayerSetting.controlType)
        {
            case EControlType.KeyboardMouse:
                KeyboardMouseSelectButton.image.color = Color.green;
                ControlpadSelectButton.image.color = Color.white;
                break;
            case EControlType.ControlPad:
                KeyboardMouseSelectButton.image.color = Color.white;
                ControlpadSelectButton.image.color = Color.green;
                break;
        }
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    //ui 오브젝트 비활성화 시킬 코루틴 함수
    private IEnumerator CloseAfterDelay(){
        animator.SetTrigger("close"); //애니메이션 가동 
        yield return new WaitForSeconds(0.5f); //애니메이션 시간만큼 기다림
        gameObject.SetActive(false); //비활성화
        animator.ResetTrigger("close"); //트랜지션 복귀시킴
    }
}
