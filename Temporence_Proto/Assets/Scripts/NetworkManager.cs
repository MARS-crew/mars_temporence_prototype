using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField IDInput;
  
    public GameObject MainMenuDirector; // 연결 안된경우 Disconnect
    public GameObject PlayDirector;
    public GameObject RespawnPanel;

    //어떤 플레이어든 한번 실행
    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);//960,540
        //동기화가 더 빠르게 해주는 부분
        PhotonNetwork.SendRate = 60; 
        PhotonNetwork.SerializationRate = 30;
        
        MainMenuDirector = GameObject.Find("MainMenuDirector");
        //PlayDirector = GameObject.Find("PlayDirector");//GameObject.Find("PlayDirector");
        RespawnPanel = GameObject.Find("Canvas").transform.Find("RespawnPanel").gameObject;
    
    }

    //연결하는 메소드 
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    //연결이 되면 실행되는 메소드
    public override void OnConnectedToMaster()
    {
        //닉네임 저장
        PhotonNetwork.LocalPlayer.NickName = IDInput.text;
        //최대인원 수를 지정
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 8 }, null);
    }

    //방에 접속이 되면 실행되는 메소드 
    public override void OnJoinedRoom()
    {
        MainMenuDirector.SetActive(false);
        PlayDirector.SetActive(true);
        Spawn();
    }

    //조인,리스폰을 버튼 눌렀을때 호출용  
    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        RespawnPanel.SetActive(false);
    }


    //매 프레임 실행되는 메소드
    void Update() { 
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) PhotonNetwork.Disconnect(); 
    }

    //연결 해제될 때 실행되는 메소드
    public override void OnDisconnected(DisconnectCause cause)
    {
        RespawnPanel.SetActive(false);
        MainMenuDirector.SetActive(true);
        PlayDirector.SetActive(false);
    }
}
