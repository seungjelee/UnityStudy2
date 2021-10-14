using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0f";
    private string userId = "깐부";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.NickName = userId;

        PhotonNetwork.SendRate = 60;
        Debug.Log(PhotonNetwork.SendRate);

        PhotonNetwork.ConnectUsingSettings();   //서버 > 로비 > 룸 순서로 진입
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandomFailed {returnCode} : {message}");
        // 룸을 생성하고 입장. 속성 설정.
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20; // 룸의 최대 접속자수
        ro.IsOpen = true;   // 룸의 오픈여부
        ro.IsVisible = true;    // 로비에서 룸 목록에 룸을 노출시킬지 여부

        PhotonNetwork.CreateRoom("My Room", ro);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"player : {player.Value.NickName}, {player.Value.ActorNumber}");
        }

        // 스폰 포인트의 랜덤한 위치에 플레이어를 스폰함
        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, points.Length);

        PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation, 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
