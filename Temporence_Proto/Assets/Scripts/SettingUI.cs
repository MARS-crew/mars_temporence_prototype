using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    //���ҽ� ��������
    [SerializeField] //����Ƽ���� ���� �����ϵ��� 
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
        //���簪 ����ǵ��� ��
        //�÷��̾� ���� Ŭ������ controltype �� ���� ���� Ȱ��ȭ�� ��ư��
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
        //�ΰ��� ���� ����Ʈ ��ư Ŭ���� ����
        //���ڿ� ���� ���۸�� ����
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

    //ui ������Ʈ ��Ȱ��ȭ ��ų �ڷ�ƾ �Լ�
    private IEnumerator CloseAfterDelay(){
        animator.SetTrigger("close"); //�ִϸ��̼� ���� 
        yield return new WaitForSeconds(0.5f); //�ִϸ��̼� �ð���ŭ ��ٸ�
        gameObject.SetActive(false); //��Ȱ��ȭ
        animator.ResetTrigger("close"); //Ʈ������ ���ͽ�Ŵ
    }
}
