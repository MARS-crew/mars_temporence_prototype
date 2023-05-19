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
  
    public GameObject MainMenuDirector; // ���� �ȵȰ�� Disconnect
    public GameObject PlayDirector;
    public GameObject RespawnPanel;

    //� �÷��̾�� �ѹ� ����
    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);//960,540
        //����ȭ�� �� ������ ���ִ� �κ�
        PhotonNetwork.SendRate = 60; 
        PhotonNetwork.SerializationRate = 30;
        
        MainMenuDirector = GameObject.Find("MainMenuDirector");
        //PlayDirector = GameObject.Find("PlayDirector");//GameObject.Find("PlayDirector");
        RespawnPanel = GameObject.Find("Canvas").transform.Find("RespawnPanel").gameObject;
    
    }

    //�����ϴ� �޼ҵ� 
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    //������ �Ǹ� ����Ǵ� �޼ҵ�
    public override void OnConnectedToMaster()
    {
        //�г��� ����
        PhotonNetwork.LocalPlayer.NickName = IDInput.text;
        //�ִ��ο� ���� ����
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 8 }, null);
    }

    //�濡 ������ �Ǹ� ����Ǵ� �޼ҵ� 
    public override void OnJoinedRoom()
    {
        MainMenuDirector.SetActive(false);
        PlayDirector.SetActive(true);
        Spawn();
    }

    //����,�������� ��ư �������� ȣ���  
    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        RespawnPanel.SetActive(false);
    }


    //�� ������ ����Ǵ� �޼ҵ�
    void Update() { 
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) PhotonNetwork.Disconnect(); 
    }

    //���� ������ �� ����Ǵ� �޼ҵ�
    public override void OnDisconnected(DisconnectCause cause)
    {
        RespawnPanel.SetActive(false);
        MainMenuDirector.SetActive(true);
        PlayDirector.SetActive(false);
    }
}
