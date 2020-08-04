using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class QuickMatchExample :  MonoBehaviour, IMatchmakingCallbacks
{
    public SceneChange scene;
    private bool change;
    [SerializeField]
    private byte maxPlayers=2;
    private LoadBalancingClient loadBalancingClient;
    public void OnCreatedRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        EnterRoomParams enterRoomParams = new EnterRoomParams();
        enterRoomParams.RoomOptions = roomOptions;
        loadBalancingClient.OpCreateRoom(enterRoomParams);
    }
    private void QuickMatch()
    {
        loadBalancingClient.OpJoinRandomRoom();
    }
    void IMatchmakingCallbacks.OnJoinRandomFailed(short returnCode, string message)
    {
        //OnCreateRoom();
    }
    public void OnCreateRoomFailed(short returnCode, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        throw new System.NotImplementedException();
    }

    public void OnJoinedRoom()
    {
        throw new System.NotImplementedException();
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnLeftRoom()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CountOfPlayersInRooms>=0&&!change)
        {
            scene.kingcreate=true;
            change = true;
        }

    }
}
