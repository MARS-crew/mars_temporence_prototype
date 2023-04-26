using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자 컨트롤 방식 정의할 열거형
public enum EControlType
{
    KeyboardMouse,
    ControlPad
}

public class PlayerSetting
{ //평범한 클래스로 쓰기위해 : MonoBehaviour제거
    public static EControlType controlType; //EControlType 타입의 전역 변수
    public static string nickname = "바람의 여행자"; 
}
