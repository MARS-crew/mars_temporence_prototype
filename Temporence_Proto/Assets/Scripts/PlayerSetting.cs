using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ��Ʈ�� ��� ������ ������
public enum EControlType
{
    KeyboardMouse,
    ControlPad
}

public class PlayerSetting
{ //����� Ŭ������ �������� : MonoBehaviour����
    public static EControlType controlType; //EControlType Ÿ���� ���� ����
    public static string nickname = "�ٶ��� ������"; 
}
